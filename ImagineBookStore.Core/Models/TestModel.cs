using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagineBookStore.Core.Models
{
    public class TestModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class TestModelValidator : AbstractValidator<TestModel>
    {
        public TestModelValidator()
        {
            RuleFor(model => model.Id).NotNull().NotEqual(0);
            RuleFor(m => m.Name).NotNull().EmailAddress();
        }
    }
}
