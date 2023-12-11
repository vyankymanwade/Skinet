

namespace API.Errors
{
    public class ApiValidationErrorResponse:ApiException
    {
        public IEnumerable<string> Errors {get;set;}
        public ApiValidationErrorResponse():base(400){

        }
    }
}