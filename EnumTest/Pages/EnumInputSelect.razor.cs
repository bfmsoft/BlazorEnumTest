using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EnumTest.Pages
{
    public class EnumInputSelectBase<TEnum> : InputBase<TEnum> where TEnum : notnull
    {
        [Parameter] public string Id { get; set; } = "";
        [Parameter] public string DefaultItemText { get; set; } = "- Select Item -";
        [Parameter] public Expression<Func<TEnum>>? ValidationFor { get; set; }
        [Parameter] public RenderFragment? ChildContent { get; set; }

        public TEnum SelectedOption { get; set; } = default!;

        protected override bool TryParseValueFromString(string value, out TEnum result, out string? validationErrorMessage)
        {
            try
            {
                result = (TEnum)Enum.Parse(typeof(TEnum), value);
                validationErrorMessage = null;

                return true;
            }
            catch (ArgumentException)
            {
                result = Enum.GetValues(typeof(TEnum)).Cast<TEnum>().First();
                validationErrorMessage = $"The {FieldIdentifier.FieldName} field is not valid.";

                return false;
            }
        }

        public List<EnumEntry> GetEnums()
        {
            var es = new List<EnumEntry>
            {
                new EnumEntry { EnumId = 1, Text = "one" },
                new EnumEntry { EnumId = 2, Text = "two" },
                new EnumEntry { EnumId = 3, Text = "three" }
            };

            return es;
        }
    }
}