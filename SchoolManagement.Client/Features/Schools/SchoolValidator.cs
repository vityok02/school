﻿using FluentValidation;
using SchoolManagement.Client.Features.Schools.Dtos;

namespace SchoolManagement.Client.Features.Schools;

public class SchoolValidator : AbstractValidator<School>
{
    public SchoolValidator()
    {
        RuleFor(s => s.Name).Length(1, 200);
        RuleFor(s => s.Country).Length(1, 100);
        RuleFor(s => s.City).Length(1, 100);
        RuleFor(s => s.Street).Length(1, 200);
        RuleFor(s => s.PostalCode).NotEqual(0);
        RuleFor(s => s.OpeningDate).NotEmpty();
    }
}
