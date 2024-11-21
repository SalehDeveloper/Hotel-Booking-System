using FluentValidation;
using HootelBooking.Application.Behaviours;
using HootelBooking.Application.Features.States.Queries.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HootelBooking.Application.Features.Countries.Queries.Search
{
    public class SearchValidator:AbstractValidator<SearchQuery> 
    {
        public SearchValidator()
        {
            RuleFor(x => x.keyword).SetValidator(new KeywordSearchValidator());
                


        }
    }
}
