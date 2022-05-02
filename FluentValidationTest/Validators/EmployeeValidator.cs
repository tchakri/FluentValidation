using FluentValidation;
using FluentValidationTest.Models;
using System;

namespace FluentValidationTest.Validators
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("First name is required.");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Last name is required.");

            RuleFor(m => m.StartDate)
                .Must(BeAValidSqlDate)
                .WithMessage("Start date is invalid.")
                .Must((d, p) => CheckStartDateWithNullAndEndDateWithValue(d.StartDate, d.EndDate))
                .WithMessage("Enter a Start Date.");

            RuleFor(m => m.EndDate)
                .Must(BeAValidSqlDate)
                .WithMessage("End date is invalid.")
                .Must((d, p) => CheckStartDateWithValueAndEndDateWithNull(d.StartDate, d.EndDate))
                .WithMessage("Enter an End Date.")
                .GreaterThan(m => m.StartDate.Value)
                .WithMessage("End Date CANNOT precede StartDate.")
                .When(m => m.StartDate.HasValue);
        }

        private bool BeAValidSqlDate(DateTime? date)
        {
            if (date.Equals(default))
            {
                return true;
            }

            return date.Value.Year > 1753;
        }

        private static bool CheckStartDateWithNullAndEndDateWithValue(DateTime? startDate, DateTime? endDate)
        {
            if ((startDate == null && endDate == null) || (startDate != null && endDate != null))
            {
                return true;
            }
            return startDate != null && endDate == null;
        }

        private static bool CheckStartDateWithValueAndEndDateWithNull(DateTime? startDate, DateTime? endDate)
        {
            if ((startDate == null && endDate == null) || (startDate != null && endDate != null))
            {
                return true;
            }
            return startDate == null && endDate != null;
        }
    }
}
