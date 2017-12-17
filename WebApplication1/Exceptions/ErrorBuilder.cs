using System;
using Nancy;
using Nancy.Responses;
using System.Threading.Tasks;

namespace WebApplication1
{
	// helper classes to report errors to consumers. 
	// Pay good attention to your error reporting. Your future self will thank you for it!
	//
	class ErrorBuilder
	{
		public static Nancy.Response ErrorResponse(string url, string verb, HttpStatusCode code, string errorMessage)
		{
			ErrorBody e = new ErrorBody
			{
				Url = url, 
				Operation = verb,
				Message = errorMessage
			};
			// Build and return an object that the Nancy server knows about.
			Nancy.Response response = new Nancy.Responses.JsonResponse<ErrorBody>(e, new DefaultJsonSerializer());
			response.StatusCode = code;
			return response;
		}
    }
	
	// useful info to return in an error
    public class ErrorBody
    {
        public string Url {get; set; }
        public string Operation { get; set; }
        public string Message { get; set; }
    }
}
