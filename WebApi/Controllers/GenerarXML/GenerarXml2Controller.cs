using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using System.Web.Http.Results;
using CapaDatos;
using CapaEstilo;
using CapaNegocios;
using efacturacionClsNuevo;
using QRCoder;
using WebApi.Models;

namespace WebApi.Controllers.GenerarXML
{
    public class GenerarXml2Controller : ApiController
    {

        public JsonResult<ClsXMLResultado> CrearXML([FromBody] ClsDataComprobante data)
        {
            // conectar a la base de datos
            var SQlserver = ConfigurationManager.AppSettings["SERVERSQL"];
            var Login = ConfigurationManager.AppSettings["LOGIN"];
            var Password = ConfigurationManager.AppSettings["PASSWORD"];
            ClsConexion go_Sql = new ClsConexion();
            string anio2 = data.anio;
            if (data.anio.Trim().Length == 4)
            {
                anio2 = anio2.Substring(2, 2);
            }
            // conexion a la base de Datos
            go_Sql.Asignar_Servidor(SQlserver, Login, Password, "COM" + data.ruc + anio2);
            // datos del comprobante 
            ClsXMLResultado resultado = new ClsXMLResultado();
            if (validarData(data))
            {
                resultado.mensaje = "No se ha enviado todas los datos";
                resultado.error = true;
                return Json(resultado);
            }
            NComprobante comp = new NComprobante();
            DataTable Dg = comp.ListaComprobanteElectronico(data.Serie, data.TipoDcumento, data.NumeroDocumento);
            CrearCarpeta(data);
            string Version;
            string OCustomId;
            string OComprobante = null;
            DateTime OFEmision;
            string OTd = null;
            string Moneda;
            //efacturacionClsNuevo.Cls_FacturaXml EFObj = new efacturacionClsNuevo.Cls_FacturaXml();
            //efacturacionClsNuevo.Cls_FacturaXml_V01 EFObjcpe = new efacturacionClsNuevo.Cls_FacturaXml_V01();
            //efacturacionClsNuevo.Cls_BoletaXML BE = new efacturacionClsNuevo.Cls_BoletaXML();
            //efacturacionClsNuevo.Cls_BoletaXML_V01 BEcpe = new efacturacionClsNuevo.Cls_BoletaXML_V01();
            //efacturacionClsNuevo.Cls_NotaCreditoXML NC = new efacturacionClsNuevo.Cls_NotaCreditoXML();
            //efacturacionClsNuevo.Cls_NotaDebitoXML Nd = new efacturacionClsNuevo.Cls_NotaDebitoXML();
            string FileNamexml = "";
            ClsEstilo lo_estilo = new ClsEstilo();

            efacturacionClsNuevo.ClsImprimirInvoice lo_imprimir = new efacturacionClsNuevo.ClsImprimirInvoice();
            string serverpath = System.Web.HttpContext.Current.Server.MapPath("~/");
            byte[] Xml_zipBinary = null;
            byte[] PDF_Binary = null;

            NPtentidad entidad = new NPtentidad();
            entidad.identidad = "001";
            entidad = entidad.Registro(entidad);

            NDet_Entidad det_Entidad = new NDet_Entidad();
            det_Entidad.IdEntidad = entidad.identidad;
            det_Entidad = det_Entidad.Item(det_Entidad);
            DataTable dt_en = entidad.itemTbl(entidad);
            string pws = null;
            pws = Desencriptar(entidad.pws);
            if (pws == "")
            {
                resultado.mensaje = "Los certificados no tienen asignados una contraseña no puede abrir el certificado";
                resultado.error = true;
                return Json(resultado);
            }
            DataTable Cabecera;
            DataTable detalle;
            NComprobante cab = new NComprobante();
            NDetalleComprobante det = new NDetalleComprobante();

            if (validar1(Dg)[1] == "1")
            {
                resultado.mensaje = validar1(Dg)[0];
                resultado.error = true;
                return Json(resultado);
            }

            cab.idtipodocumento = Dg.Rows[0]["IdTipoDocumento"].ToString();
            cab.serie = Dg.Rows[0]["Serie"].ToString();
            cab.numerodocumento = Dg.Rows[0]["NumeroDocumento"].ToString();

            det.idtipodocumento = Dg.Rows[0]["IdTipoDocumento"].ToString();
            det.serie = Dg.Rows[0]["Serie"].ToString();
            det.numerodocumento = Dg.Rows[0]["NumeroDocumento"].ToString();

            bool iscpe = false;

            if (Dg.Rows[0]["AfecIGV"].ToString() == "")
            {
                Cabecera = cab.cabeceraCPE21cpe(cab);
                detalle = det.DetalleCPE21CPE(det);
                iscpe = true;
            }
            else
            {
                Cabecera = cab.cabeceraCPE21(cab);
                detalle = det.DetalleCPE21(det);
                iscpe = false;
            }
            NArticulo na = new NArticulo();
            if (na.TieneCodigoSunat(detalle) == false)
            {
                resultado.mensaje = "Existen productos en el comprobante que no tienen código producto sunat(Obligatorio a partir del 01 Enero 2020), favor de registrarlo en la opción principal/Articulo/Articulo";
                resultado.error = true;
                return Json(resultado);
            }
            if (entidad.identidad == "001")
            {
                if (Dg.Rows.Count > 0)
                {
                    byte[] efact = null;
                    {
                        Version = "2.1"; OCustomId = "2.0";
                        //deletefile(Application.StartupPath + @"\tempxml");
                        OTd = Dg.Rows[0]["TdSunat"].ToString();
                        OComprobante = Dg.Rows[0]["Serie"].ToString().Trim() + "-" + Dg.Rows[0]["NumeroDocumento"].ToString().Trim();
                        OFEmision = Convert.ToDateTime(Dg.Rows[0]["FechaDocumento"].ToString());
                        FileNamexml = entidad.ruc + "-" + OTd + "-" + OComprobante;
                        if (Dg.Rows[0]["IdMoneda"].ToString().Trim() == "MN")
                            Moneda = "PEN";
                        else
                            Moneda = "USD";

                        if (OTd == "01")
                        {
                            if (iscpe == true)
                            {
                                EFObjcpe.Pro_Moneda(Dg.Rows[0]["IdMoneda"].ToString());
                                efact = EFObjcpe.CreateInvoice("", entidad.ruc, Version, OCustomId, OComprobante, OFEmision, OTd, Moneda, dt_en, Cabecera, detalle);
                            }
                            else
                            {
                                EFObj.Pro_Moneda(Dg.Rows[0]["IdMoneda"].ToString());
                                efact = EFObj.CreateInvoice("", entidad.ruc, Version, OCustomId, OComprobante, OFEmision, OTd, Moneda, dt_en, Cabecera, detalle);
                            }
                        }
                        if (OTd == "03")
                        {
                            if (iscpe == true)
                            {
                                BEcpe.Pro_Moneda(Dg.Rows[0]["IdMoneda"].ToString());
                                efact = BEcpe.CreateInvoice("", entidad.ruc, Version, OCustomId, OComprobante, OFEmision, OTd, Moneda, dt_en, Cabecera, detalle);
                            }
                            else
                            {
                                BE.Pro_Moneda(Dg.Rows[0]["IdMoneda"].ToString());
                                efact = BE.CreateInvoice("", entidad.ruc, Version, OCustomId, OComprobante, OFEmision, OTd, Moneda, dt_en, Cabecera, detalle);
                            }
                        }

                        if (OTd == "07")
                        {
                            NC.Pro_Moneda(Dg.Rows[0]["IdMoneda"].ToString());
                            efact = NC.CreateInvoice("", entidad.ruc, Version, OCustomId, OComprobante, OFEmision, OTd, Moneda, dt_en, Cabecera, detalle);
                        }

                        if (OTd == "08")
                        {
                            Nd.Pro_Moneda(Dg.Rows[0]["IdMoneda"].ToString());
                            efact = Nd.CreateInvoice("", entidad.ruc, Version, OCustomId, OComprobante, OFEmision, OTd, Moneda, dt_en, Cabecera, detalle);
                        }

                        try
                        {

                            X509Certificate2 Key;
                            if (det_Entidad.pfx_file != null)
                                Key = new X509Certificate2(det_Entidad.pfx_file, pws);
                            else
                                Key = new X509Certificate2(entidad.rutapfx, pws);
                            X509Certificate2 Key1;
                            if (det_Entidad.cer_file != null)
                                Key1 = new X509Certificate2(det_Entidad.cer_file, pws);
                            else
                                Key1 = new X509Certificate2(entidad.rutacer, pws);

                            // Firma del XML
                            eFacturacionCls.ClsFirma cls_firma = new eFacturacionCls.ClsFirma();
                            cls_firma.firmaBinari(efact, serverpath + @"\sigma\" + data.ruc + @"\xml\" + FileNamexml + ".xml", Key, false);
                            string has = null;
                            string Signature = null;
                            has = cls_firma.ReturCodHas();
                            Signature = cls_firma.Retursignaturevalue();

                            NComprobante co = new NComprobante();
                            co.idagencia = Dg.Rows[0]["IdAgencia"].ToString();
                            co.idtipodocumento = Dg.Rows[0]["IdTipoDocumento"].ToString();
                            co.serie = Dg.Rows[0]["Serie"].ToString();
                            co.numerodocumento = Dg.Rows[0]["NumeroDocumento"].ToString();
                            co.idalmacen = Dg.Rows[0]["IdAlmacen"].ToString();
                            co.estadosunat = "1";
                            co.signaturevalue = Signature;
                            co.codigohas = has;
                            co.CodigoBarraras(co);
                            Bitmap bm = null;
                            string cadenaBarra = entidad.ruc + "|" + OTd + "|" + Dg.Rows[0]["Serie"].ToString().Trim() + "|" + Dg.Rows[0]["NumeroDocumento"].ToString().Trim() + "|" + Dg.Rows[0]["importeIGV"].ToString() + "|" + Dg.Rows[0]["ImporteTotal"].ToString() + "|" + Dg.Rows[0]["FechaDocumento"].ToString() + "|" + Dg.Rows[0]["TipoDocSunat"].ToString().Trim() + "|" + Dg.Rows[0]["Ruc"].ToString().Trim() + "|";
                            bm = (Bitmap)QRbarra(cadenaBarra);
                            var valor = Dg.Rows[0]["IdAgencia"].ToString().Trim() + Dg.Rows[0]["IdAlmacen"].ToString().Trim() + Dg.Rows[0]["IdTipoDocumento"].ToString().Trim() + Dg.Rows[0]["Serie"].ToString().Trim() + Dg.Rows[0]["NumeroDocumento"].ToString().Trim();
                            go_Sql.saveimagen("Comprobante ", "barrapdf417", "Rtrim(IdAgencia)+rtrim(IdAlmacen)+Rtrim(IdTipoDocumento)+rtrim(Serie)+rtrim(NumeroDocumento)", valor, lo_estilo.Image2Bytes(bm));

                            bool result = cls_firma.VerifyXmlFile_509(serverpath + @"\sigma\" + data.ruc + @"\xml\" + FileNamexml + ".xml", Key1);
                            if (result == false)
                            {
                                resultado.mensaje = "La firma esta adulterada";
                                resultado.error = true;
                                return Json(resultado);
                            }
                            ClsZIP Zip = new ClsZIP();
                            Xml_zipBinary = Zip.ComprimirToBinary(serverpath + @"\sigma\" + data.ruc + @"\xml\", FileNamexml + ".xml", FileNamexml + ".zip");
                        }
                        catch (CryptographicException ex)
                        {
                            resultado.mensaje = "Error";
                            resultado.error = true;
                            resultado.errormensaje = ex.Message.ToString();
                            return Json(resultado);
                        }
                    }
                }
            }
            // empieza  generar
            // 
            // 
           
            // Dim archivo As New NComprobante_CPE
            string ruta = serverpath + @"\sigma\" + data.ruc + @"\";
            Directory.CreateDirectory(Path.GetDirectoryName(ruta));
            if (Xml_zipBinary != null || PDF_Binary != null)
            {
                File.WriteAllBytes(ruta + @"\zip\" + FileNamexml + ".zip", Xml_zipBinary);

                //File.WriteAllBytes(ruta + @"\pdf\" + FileNamexml + ".pdf", PDF_Binary);
            }
            resultado.mensaje = "Se Ha generado con éxito el  Xml";
            resultado.error = false;
            return Json(resultado);
        }
        public string Desencriptar(string aString)
        {
            string st = "";
            int i;
            for (i = 0; i <= aString.Length - 1; i++)
                st += Denc(Convert.ToChar(aString.Substring(i, 1)));
            return st;
        }
        private char Denc(char aChar)
        {
            char ctem;
            bool minuscula = false;
            if (char.IsLower(aChar))
            {
                minuscula = true;
                aChar = char.ToUpper(aChar);
            }
            ctem = '-';
            switch (aChar)
            {
                case object _ when aChar == 'Y':
                    {
                        ctem = 'A';
                        break;
                    }

                case object _ when aChar == 'S':
                    {
                        ctem = 'B';
                        break;
                    }

                case object _ when aChar == 'A':
                    {
                        ctem = 'C';
                        break;
                    }

                case object _ when aChar == 'R':
                    {
                        ctem = 'D';
                        break;
                    }

                case object _ when aChar == 'X':
                    {
                        ctem = 'E';
                        break;
                    }

                case object _ when aChar == 'B':
                    {
                        ctem = 'F';
                        break;
                    }

                case object _ when aChar == 'T':
                    {
                        ctem = 'G';
                        break;
                    }

                case object _ when aChar == 'F':
                    {
                        ctem = 'H';
                        break;
                    }

                case object _ when aChar == 'H':
                    {
                        ctem = 'I';
                        break;
                    }

                case object _ when aChar == 'L':
                    {
                        ctem = 'J';
                        break;
                    }

                case object _ when aChar == 'O':
                    {
                        ctem = 'K';
                        break;
                    }

                case object _ when aChar == 'P':
                    {
                        ctem = 'L';
                        break;
                    }

                case object _ when aChar == 'Ñ':
                    {
                        ctem = 'M';
                        break;
                    }

                case object _ when aChar == 'C':
                    {
                        ctem = 'N';
                        break;
                    }

                case object _ when aChar == 'D':
                    {
                        ctem = 'Ñ';
                        break;
                    }

                case object _ when aChar == 'G':
                    {
                        ctem = 'O';
                        break;
                    }

                case object _ when aChar == 'I':
                    {
                        ctem = 'P';
                        break;
                    }

                case object _ when aChar == 'W':
                    {
                        ctem = 'Q';
                        break;
                    }

                case object _ when aChar == 'Z':
                    {
                        ctem = 'R';
                        break;
                    }

                case object _ when aChar == 'K':
                    {
                        ctem = 'S';
                        break;
                    }

                case object _ when aChar == 'V':
                    {
                        ctem = 'T';
                        break;
                    }

                case object _ when aChar == 'E':
                    {
                        ctem = 'U';
                        break;
                    }

                case object _ when aChar == 'M':
                    {
                        ctem = 'V';
                        break;
                    }

                case object _ when aChar == 'N':
                    {
                        ctem = 'W';
                        break;
                    }

                case object _ when aChar == 'J':
                    {
                        ctem = 'X';
                        break;
                    }

                case object _ when aChar == 'Q':
                    {
                        ctem = 'Y';
                        break;
                    }

                case object _ when aChar == 'U':
                    {
                        ctem = 'Z';
                        break;
                    }

                case object _ when aChar == '(':
                    {
                        ctem = '0';
                        break;
                    }

                case object _ when aChar == '*':
                    {
                        ctem = '1';
                        break;
                    }

                case object _ when aChar == '[':
                    {
                        ctem = '2';
                        break;
                    }

                case object _ when aChar == ')':
                    {
                        ctem = '3';
                        break;
                    }

                case object _ when aChar == '$':
                    {
                        ctem = '4';
                        break;
                    }

                case object _ when aChar == '#':
                    {
                        ctem = '5';
                        break;
                    }

                case object _ when aChar == '.':
                    {
                        ctem = '6';
                        break;
                    }

                case object _ when aChar == ']':
                    {
                        ctem = '7';
                        break;
                    }

                case object _ when aChar == '+':
                    {
                        ctem = '8';
                        break;
                    }

                case object _ when aChar == '{':
                    {
                        ctem = '9';
                        break;
                    }

                case object _ when aChar == '9':
                    {
                        ctem = '&';
                        break;
                    }

                case object _ when aChar == '&':
                    {
                        ctem = '*';
                        break;
                    }

                case object _ when aChar == '6':
                    {
                        ctem = '+';
                        break;
                    }

                case object _ when aChar == '4':
                    {
                        ctem = '.';
                        break;
                    }

                case object _ when aChar == '8':
                    {
                        ctem = '8';
                        break;
                    }

                case object _ when aChar == '2':
                    {
                        ctem = '2';
                        break;
                    }

                case object _ when aChar == '3':
                    {
                        ctem = '3';
                        break;
                    }

                case object _ when aChar == '-':
                    {
                        ctem = '-';
                        break;
                    }

                case object _ when aChar == '5':
                    {
                        ctem = '5';
                        break;
                    }

                case object _ when aChar == '7':
                    {
                        ctem = '7';
                        break;
                    }

                case object _ when aChar == '0':
                    {
                        ctem = '0';
                        break;
                    }

                case object _ when aChar == '?':
                    {
                        ctem = '$';
                        break;
                    }

                case object _ when aChar == '@':
                    {
                        ctem = '#';
                        break;
                    }

                case object _ when aChar == '}':
                    {
                        ctem = '-';
                        break;
                    }

                case object _ when aChar == '1':
                    {
                        ctem = '@';
                        break;
                    }

                case object _ when aChar == '%':
                    {
                        ctem = '%';
                        break;
                    }

                default:
                    {
                        ctem = aChar;
                        break;
                    }
            }
            if (minuscula == true)
                ctem = char.ToLower(ctem);
            return ctem;
        }
        private string[] validar1(DataTable Dg)
        {
            string[] mensaje = new string[3];
            mensaje[0] = "Los datos son conformes";
            mensaje[1] = "0";
            if (Dg.Rows[0]["TdSunat"].ToString().Trim() == "")
            {
                mensaje[0] = "El Tipo de Documento no es valido, favor de configurar en tablas generales";
                mensaje[1] = "1";
                return mensaje;
            }
            if (Dg.Rows[0]["TipoDocSunat"].ToString().Trim() == "")
            {
                mensaje[0] = "El Tipo de Documento de identidad del cliente no es valido, favor de configurar en tablas generales";
                mensaje[1] = "1";
                return mensaje;
            }

            if (Dg.Rows[0]["EstadoSunat"].ToString().Trim() == "2")
            {
                mensaje[0] = "No se puede volver a generar el documento ya se envió a Sunat";
                mensaje[1] = "1";
                return mensaje;
            }
            return mensaje;
        }
        private Image QRbarra(string cadena_barra)
        {
            string level = "Q";
            QRCodeGenerator.ECCLevel eccLevel = new QRCodeGenerator.ECCLevel();
            eccLevel = (QRCodeGenerator.ECCLevel)(level == "L" ? 0 : level == "M" ? 1 : level == "Q" ? 2 : 3);
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(cadena_barra, eccLevel))
                {
                    using (QRCode qrCode = new QRCode(qrCodeData))
                    {
                        return qrCode.GetGraphic(20, Color.Black, Color.White, GetIconBitmap(), System.Convert.ToInt32(0));
                    }
                }
            }
        }
        private Bitmap GetIconBitmap()
        {
            string iconPath = "";
            Bitmap img = null/* TODO Change to default(_) if this is not a reference type */;
            if (iconPath.Length > 0)
            {
                try
                {
                    img = new Bitmap(iconPath);
                }
                catch
                {
                    return null;
                }
            }
            return img;
        }
        private void CrearCarpeta(ClsDataComprobante data)
        {
            string serverPath = System.Web.HttpContext.Current.Server.MapPath("~/sigma/");
            if (!Directory.Exists(Path.GetDirectoryName(serverPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(serverPath));
            }
            serverPath = System.Web.HttpContext.Current.Server.MapPath("~/sigma/" + data.ruc + "/");
            if (!Directory.Exists(Path.GetDirectoryName(serverPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(serverPath));
            }
            serverPath = System.Web.HttpContext.Current.Server.MapPath("~/sigma/" + data.ruc + "/pdf/");
            if (!Directory.Exists(Path.GetDirectoryName(serverPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(serverPath));
            }
            serverPath = System.Web.HttpContext.Current.Server.MapPath("~/sigma/" + data.ruc + "/xml/");
            if (!Directory.Exists(Path.GetDirectoryName(serverPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(serverPath));
            }
            serverPath = System.Web.HttpContext.Current.Server.MapPath("~/sigma/" + data.ruc + "/zip/");
            if (!Directory.Exists(Path.GetDirectoryName(serverPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(serverPath));
            }
            serverPath = System.Web.HttpContext.Current.Server.MapPath("~/sigma/" + data.ruc + "/cdr/");
            if (!Directory.Exists(Path.GetDirectoryName(serverPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(serverPath));
            }
        }
        private bool validarData(ClsDataComprobante data)
        {
            bool estado = false;
            if (data.ruc == null || data.ruc == "")
            {
                estado = true;
            }
            if (data.anio == null || data.anio == "")
            {
                estado = true;
            }
            if (data.TipoDcumento == null || data.TipoDcumento == "")
            {
                estado = true;
            }
            if (data.NumeroDocumento == null || data.NumeroDocumento == "")
            {
                estado = true;
            }
            if (data.Serie == null || data.Serie == "")
            {
                estado = true;
            }
            return estado;
        }
    }
}
