using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArpaMedia.Web.Api.Models
{
    public class BadResponse: ResponseBase
    {
        public Dictionary<String, String[]> Errors { get; set; }

        public BadResponse()
        {
            this.StatusCode = StatusCodes.Status400BadRequest;
        }

        public BadResponse(String error)
        {
            this.StatusCode = StatusCodes.Status400BadRequest;
            this.AddResponseError(error);
        }

        public BadResponse(ModelStateDictionary modelState)
        {
            Errors = modelState.ToDictionary(
                   kvp => kvp.Key.Replace("$.", ""),
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
            );
        }

        public void AddResponseError(string key, String[] values)
        {
            if (Errors == null)
            {
                Errors = new Dictionary<String, String[]>();
            }
            Errors.Add(key, values);
        }

        public void AddResponseError(String error)
        {
            if (Errors == null)
            {
                Errors = new Dictionary<String, String[]>();
            }

            Errors.Add("message", new string[] { error });
        }

        public void AddResponseError(String[] values)
        {
            if (Errors == null)
            {
                Errors = new Dictionary<String, String[]>();
            }
            Errors.Add("message", values);
        }
    }
}
