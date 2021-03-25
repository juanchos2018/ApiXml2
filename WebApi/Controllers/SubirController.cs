using CloudinaryDotNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class SubirController : ApiController
    {

        [HttpPost]
        
        public HttpResponseMessage Index()
        {
            string fullpath = "";
            var supportedTypes = new List<string> { "pdf", "zip", "xml" };
            if (!Request.Content.IsMimeMultipartContent())
            {
                return Request.CreateErrorResponse(HttpStatusCode.UnsupportedMediaType, "La solicitud no incluye contenido válido");
            }
            try
            {
                var file = HttpContext.Current.Request.Files.Count > 0 ?  HttpContext.Current.Request.Files[0] : null;
                if (file != null)
                {
                    var serverPath = "";
                    var path = "";
                    string ext = "";
                    char primera ;
                    string a = "";
                    string fileName = "";
                    
                    if(file.FileName != null)
                    {
                        primera = file.FileName[0];
                        a = primera.ToString();
                        ext = Path.GetExtension(file.FileName);
                        fileName=file.FileName;
                    }

                    // crea directorio
                    path = System.Web.HttpContext.Current.Server.MapPath("~/sigma/");
                    if (!Directory.Exists(Path.GetDirectoryName(path)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(path));
                    }

                    switch (ext)
                    {

                        case ".pdf":

                            string path_ruc1 = "";
                            string[] words1 = fileName.Split('-');
                            string nameRuc1 = words1[0];
                            path_ruc1 = System.Web.HttpContext.Current.Server.MapPath("~/sigma/" + nameRuc1 + "/");
                            if (!Directory.Exists(Path.GetDirectoryName(path_ruc1)))
                            {
                                Directory.CreateDirectory(Path.GetDirectoryName(path_ruc1));
                            }

                            serverPath = System.Web.HttpContext.Current.Server.MapPath("~/sigma/" + nameRuc1 + "/pdf/");
                            if (!Directory.Exists(Path.GetDirectoryName(serverPath)))
                            {
                                Directory.CreateDirectory(Path.GetDirectoryName(serverPath));
                            }

                            break;

                        case ".xml":

                            string path_ruc2 = "";
                            string[] words2 = fileName.Split('-');
                            string nameRuc2 = words2[0];
                            path_ruc2 = System.Web.HttpContext.Current.Server.MapPath("~/sigma/" + nameRuc2 + "/");
                            if (!Directory.Exists(Path.GetDirectoryName(path_ruc2)))
                            {
                                Directory.CreateDirectory(Path.GetDirectoryName(path_ruc2));
                            }

                            serverPath = System.Web.HttpContext.Current.Server.MapPath("~/sigma/" + nameRuc2 + "/xml/");
                            if (!Directory.Exists(Path.GetDirectoryName(serverPath)))
                            {
                                Directory.CreateDirectory(Path.GetDirectoryName(serverPath));
                            }
                            
                            break;
                        case ".zip":
                            if (a.Equals("R") || a.Equals("r"))
                            {
                                // para cdr
                                string path_ruc = "";
                                string[] words = fileName.Split('-');
                                string nameRuc = words[1];
                                path_ruc = System.Web.HttpContext.Current.Server.MapPath("~/sigma/" + nameRuc + "/");
                                if (!Directory.Exists(Path.GetDirectoryName(path_ruc)))
                                {
                                    Directory.CreateDirectory(Path.GetDirectoryName(path_ruc));
                                }

                                serverPath = System.Web.HttpContext.Current.Server.MapPath("~/sigma/" + nameRuc + "/cdr/");
                                if (!Directory.Exists(Path.GetDirectoryName(serverPath)))
                                {
                                    Directory.CreateDirectory(Path.GetDirectoryName(serverPath));
                                }

                            }
                            else
                            {
                                string path_ruc = "";
                                string[] words = fileName.Split('-');
                                string nameRuc = words[0];
                                path_ruc = System.Web.HttpContext.Current.Server.MapPath("~/sigma/" + nameRuc + "/");
                                if (!Directory.Exists(Path.GetDirectoryName(path_ruc)))
                                {
                                    Directory.CreateDirectory(Path.GetDirectoryName(path_ruc));
                                }

                                serverPath = System.Web.HttpContext.Current.Server.MapPath("~/sigma/" + nameRuc + "/zip/");
                                if (!Directory.Exists(Path.GetDirectoryName(serverPath)))
                                {
                                    Directory.CreateDirectory(Path.GetDirectoryName(serverPath));
                                }

                            }

                            break;

                        default:
                            break;
                    }

                    if (!supportedTypes.Contains(ext.Trim().TrimStart('.')))
                        throw new ArgumentException("El tipo de archivo proporcionado no es compatible!");

                    fullpath = Path.Combine(serverPath, Path.GetFileName(fileName));
                    file.SaveAs(fullpath);
                }
                var response = Request.CreateResponse(HttpStatusCode.OK);
                response.StatusCode.ToString();
                return response;
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
           }
       }

    }
}
