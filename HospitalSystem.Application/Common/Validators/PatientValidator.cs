using FluentValidation;
using HospitalSystem.Application.Common.Interfaces;
using HospitalSystem.Application.Patients.DTOs;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HospitalSystem.Application.Common.Validators;

public class PatientValidator : AbstractValidator<CreatePatientDto>
{
    private readonly IApplicationDbContext _context;

    public PatientValidator(IApplicationDbContext context)
    {
        _context = context;
        // IDENTITY
        RuleFor(x => x.NationalId)
            .NotEmpty().WithMessage("National Id or Identification Number")
            .Matches(@"^\d{7,10}$").WithMessage("Invalid identifcation Numnber format")
            .MustAsync(BeUniqueNationalIdAsync).WithMessage("This identification Number is already registered");

        // Names 
        RuleFor(x => x.FirstName).NotEmpty().MinimumLength(2).MaximumLength(100);
        RuleFor(x => x.LastName).NotEmpty().MinimumLength(2).MaximumLength(100);

        // --Kenyan Phone validation (M-pesa  validation)
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .Matches(@"^(?:254|\+254|0)?(7|1)\d{8}$").WithMessage("Invalid Kenyan phone number")
            .MustAsync(BeUniquePhoneNumberAsync).WithMessage("Phone number already exists");

        // --Data consent
        RuleFor(x => x.DataProcessingConsent)
            .Equal(true).WithMessage("Patient must consent to data processing");
        // -- Insurance
        RuleFor(x => x.SHANumber)
            .NotEmpty().WithMessage("SHA number is mandatory");
        // --Remaining Rules
        RuleFor(x => x.DateOfBirth)
            .NotEmpty()
            .Must(g => new []{"Male", "Female", "Other"}.Contains(g));

        

        

    }    
    private async Task<bool> BeUniqueNationalIdAsync(string nationalId, CancellationToken ct)
    {
        return !await _context.Patients.AnyAsync(p => p.NationalId == nationalId, ct);

    }
    private async Task<bool> BeUniquePhoneNumberAsync(string phoneNumber, CancellationToken ct)
    {
        return !await _context.Patients.AnyAsync(p => p.PhoneNumber == phoneNumber, ct);
        
    }
    private async Task<bool> BeUniqueEmailAsync(string email, CancellationToken ct)
    {
        if(string.IsNullOrEmpty(email)) return true;
        return !await _context.Patients.AnyAsync(p => p.Email == email, ct);
    }

    // Public static method for CustomValidation attribute
    public static ValidationResult ValidateEmailUniqueness(string email, ValidationContext context)
    {
        if (string.IsNullOrEmpty(email))
        {
            return ValidationResult.Success;
        }

        // Note: This is a synchronous validation method. For async validation, 
        // it's recommended to use FluentValidation rules instead of CustomValidation attribute.
        // This method serves as a placeholder for immediate format validation.
        
        if (!System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            return new ValidationResult("Invalid email format");
        }

        return ValidationResult.Success;
    }

    
}
