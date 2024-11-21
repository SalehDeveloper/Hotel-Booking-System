using FluentValidation;
using HootelBooking.Application.Behaviours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.States.Queries.Search
{
    public class StateSearchValidator:AbstractValidator<StateSearchQuery>
    {
        public StateSearchValidator()
        {
                RuleFor(x=> x.keywrod).SetValidator(new KeywordSearchValidator());  
        }
    }
}
