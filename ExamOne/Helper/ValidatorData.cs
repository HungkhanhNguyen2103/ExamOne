using Azure;
using ExamOne.Models;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ExamOne.Helper
{
    public class ValidatorData
    {
        public static ResponderData<string> CheckRequiredFields(object model)
        {
            var responder = new ResponderData<string>();

            if (model == null)
            {
                responder.Message = "Thông tin không hợp lệ";
                return responder;
            }

            var stringProperties = model.GetType()
                                        .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                        .Where(p => p.PropertyType == typeof(string));

            foreach (var prop in stringProperties)
            {
                var value = prop.GetValue(model) as string;
                if (string.IsNullOrWhiteSpace(value))
                {
                    var displayAttr = prop.GetCustomAttribute<DisplayAttribute>();
                    var displayName = displayAttr != null ? displayAttr.Name : prop.Name;
                    responder.Message = $"{displayName} không được để trống";
                    return responder;
                }
            }
            responder.IsSuccess = true;
            return responder;
        }
    }
}
