using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;

namespace WebApiApp.Utilities
{
    public static class ModelStateDictionaryExtensions
    {
        public static List<string> GetErrorsAsList(this ModelStateDictionary modelState)
        {
            if (modelState == null || modelState.Values == null)
            {
                return new List<string>();
            }

            IList<string> allErrors = modelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage)).ToList();
            var err = allErrors.Where(error => !string.IsNullOrEmpty(error)).ToList();
            if (err.Count == 0)
            {
                err = modelState.Values.SelectMany(v => v.Errors.Select(b => b.Exception.Message)).ToList();
            }

            return err;
        }
    }
}