﻿namespace SchoolManagement.API.Features.Students.Dtos;

public record StudentCreateDto(
    string FirstName,
    string LastName,
    int Age,
    string Group)
    : IStudentDto;
