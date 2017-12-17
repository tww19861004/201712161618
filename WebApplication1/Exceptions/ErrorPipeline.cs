using System.Text;
using Nancy;
using Nancy.Bootstrapper;
using HttpStatusCode = Nancy.HttpStatusCode;

namespace WebApplication1
{
    public class ErrorPipeline : IApplicationStartup
    {
        //Hook all kind of errors here
        //we are simply defined one custom exception for demo
        public void Initialize(IPipelines pipelines)
        {
            pipelines.OnError += (context, exception) =>
            {
                if (exception is ServerDataNotFoundException)
                {
                    return new Response
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        ContentType = "text/html",
                        Contents = (stream) =>
                        {
                            var errorMessage =
                                Encoding.UTF8.GetBytes(exception.Message);
                            stream.Write(errorMessage, 0, errorMessage.Length);
                        }
                    };
                }
                else
                {
                    return new Response
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        ContentType = "text/html",
                        Contents = (stream) =>
                        {
                            var errorMessage =
                                Encoding.UTF8.GetBytes("Initialize.pipelines.OnError:" + exception.StackTrace);
                            stream.Write(errorMessage, 0, errorMessage.Length);
                        }
                    };
                }               

                //If not expected exception simply throw 500 exception
                return HttpStatusCode.InternalServerError;
            };
        }
    }
}