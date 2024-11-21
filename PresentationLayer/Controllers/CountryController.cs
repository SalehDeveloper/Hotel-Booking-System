using BLL.DTOs;
using BLL.Services;
using DAC.Data;
using DAC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Net;

namespace PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController(ICountryService _service, AppDbContext _context) : ControllerBase
    {


        [HttpPost]
        public async Task<APIResponse<CountryResponseDto>> AddNewCountry([FromBody] CreateCountryRequestDto dto)
        {

            if (!ModelState.IsValid)
            {
                return new APIResponse<CountryResponseDto>(HttpStatusCode.BadRequest, "Invalid inputs");
            }

            try
            {
                var res = await _service.CreateAsync(dto);

                if (res is not null)

                    return new APIResponse<CountryResponseDto>(res, "New Country Added Successfully");

                
            }
            catch (Exception ex)
            {

                return new APIResponse<CountryResponseDto>(HttpStatusCode.InternalServerError, ex.Message);
            }

            return new APIResponse<CountryResponseDto>(HttpStatusCode.InternalServerError, "An error Ocurred while creating the Country");
        }


        [HttpPut("{id}")]
        public async Task<APIResponse<CountryResponseDto>> Update(int id, [FromBody] CreateCountryRequestDto dto)
        {

            if (!ModelState.IsValid)
                return new APIResponse<CountryResponseDto>(HttpStatusCode.BadRequest, "Invalid inputs");

            try
            {
                var res = await _service.UpdateAsync(id, dto);

                if (res is not null)
                {

                  
                    return new APIResponse<CountryResponseDto>(res, "Updated Successfully");

                }

                return new APIResponse<CountryResponseDto>(HttpStatusCode.NotFound, $"Country With ID {id} Not Found");
            }
            catch (Exception ex)
            {
                return new APIResponse<CountryResponseDto>(HttpStatusCode.InternalServerError, ex.Message);

            }



        }



        [HttpDelete("{id}")]
        public async Task<APIResponse<int>> Delete(int id)
        {

            var countryToDelete = await _context.Countries.FindAsync(id);

            if (countryToDelete != null)
            {
                if (!countryToDelete.IsActive)
                    return new APIResponse<int>(-1, $"Country with id {id} Already Deleted ", HttpStatusCode.NoContent);
            }

            try
            {
                var res = await _service.DeleteAsync(id);

                if (res)
                    return new APIResponse<int>(id, "Deleted Successfully");

                return new APIResponse<int>(-1, $"Country with id {id} Not Found", HttpStatusCode.BadRequest);
            }

            catch (Exception ex)
            {

                return new APIResponse<int>(-1, ex.Message, HttpStatusCode.InternalServerError);

            }


        }

        [HttpGet("id/{id}")]

        public async Task<APIResponse<CountryResponseDto>> GetByID(int id)
        {
            try
            {
                var res = await _service.GetByIdAsync(id);


                if (res is not null)
                {
                    return new APIResponse<CountryResponseDto>(res, "Country Retreived Successfully");

                }
            }
            catch (Exception ex)
            {

                return new APIResponse<CountryResponseDto>(HttpStatusCode.InternalServerError, ex.Message);


            }

            return new APIResponse<CountryResponseDto>(HttpStatusCode.NotFound, $"Country With Id {id} Not found");





        }


        [HttpGet("name/{name}")]

        public async Task<APIResponse<CountryResponseDto>> GetByName(string name)
        {


            try
            {

                var res = await _service.GetByNameAsync(name);



                if (res is not null)

                {

                    
                    return new APIResponse<CountryResponseDto>(res, "Country Retreived Successfully");

                }
            }
            catch (Exception ex)
            {

                return new APIResponse<CountryResponseDto>(HttpStatusCode.InternalServerError, ex.Message);

            }

            return new APIResponse<CountryResponseDto>(HttpStatusCode.NotFound, $"Country With name {name} Not found");
        }


        [HttpGet("code/{code}")]
        public async Task<APIResponse<CountryResponseDto>> GetByCode(string code)
        {
            try
            {

                var res = await _service.GetByCodeAsync(code);

                if (res is not null)
                {
                    

                    return new APIResponse<CountryResponseDto>(res, "Retreived Successfully");
                }

                return new APIResponse<CountryResponseDto>(HttpStatusCode.NotFound, $"Country with code {code} Not Found");
            }
            catch (Exception ex)
            {
                return new APIResponse<CountryResponseDto>(HttpStatusCode.InternalServerError, ex.Message);

            }
        }

        [HttpGet]
        [Route("Active")]
        public async Task<APIResponse<IEnumerable<CountryResponseDto>>> GetAllActive()
        {
            try
            {
                var res = await _service.GetAllActiveAsync();

                if (res.Any())
                {
                

                    return new APIResponse<IEnumerable<CountryResponseDto>>(res, "Countries Retreived Successfully");

                }

            }
            catch (Exception ex)
            {
                return new APIResponse<IEnumerable<CountryResponseDto>>(HttpStatusCode.InternalServerError, ex.Message);


            }




            return new APIResponse<IEnumerable<CountryResponseDto>>(HttpStatusCode.NotFound, "No Active Countries Found");
        }

        [HttpGet]
        [Route("InActive")]
        public async Task<APIResponse<IEnumerable<CountryResponseDto>>> GetAllInActive()
        {

            try
            {
                var res = await _service.GetAllInActiveAsync();

                if (res.Any())
                {
                  

                    return new APIResponse<IEnumerable<CountryResponseDto>>(res, "Countries Retreived Successfully");
                }

            }
            catch (Exception ex)
            {


                return new APIResponse<IEnumerable<CountryResponseDto>>(HttpStatusCode.InternalServerError, ex.Message);

            }

            return new APIResponse<IEnumerable<CountryResponseDto>>(HttpStatusCode.NotFound, "No InActive Countries Found");
        }

        [HttpGet]
        [Route("All")]
        public async Task<APIResponse<IEnumerable<CountryResponseDto>>> GetAll()
        {
            try
            {
                var res = await _service.GetAllAsync();

                if (!res.Any() || res is null)
                    return new APIResponse<IEnumerable<CountryResponseDto>>(HttpStatusCode.NoContent, "No Countries Found");
             

                return new APIResponse<IEnumerable<CountryResponseDto>>(res, "Retreived Successfully");
            }

            catch (Exception ex)
            {

                return new APIResponse<IEnumerable<CountryResponseDto>>(HttpStatusCode.InternalServerError, ex.Message);

            }



        }

        [HttpGet]
        [Route("AllStates/{id}")]
        public async Task<APIResponse<IEnumerable<StateResponseDto>>> GetAllStatesInById (int id)
        {
            var states = await _service.GetAllStatesInById(id);

            if (states is null )
                return new APIResponse<IEnumerable<StateResponseDto>>(HttpStatusCode.NotFound, $"Country With ID {id} Not Found");

            if (!states.Any())
                return new APIResponse<IEnumerable<StateResponseDto>>(HttpStatusCode.NoContent, $"Country has No States");

            return new APIResponse<IEnumerable<StateResponseDto>>(states, "Retreived Successfully");

        }

        [HttpGet]
        [Route("AllStatesBy/{name}")]
        public async Task<APIResponse<IEnumerable<StateResponseDto>>> GetAllStatesInById(string name )
        {
            var states = await _service.GetAllStatesInByName(name);

            if (states is null)
                return new APIResponse<IEnumerable<StateResponseDto>>(HttpStatusCode.NotFound, $"Country With name {name} Not Found");

            if (!states.Any())
                return new APIResponse<IEnumerable<StateResponseDto>>(HttpStatusCode.NoContent, $"Country has No States");

            return new APIResponse<IEnumerable<StateResponseDto>>(states, "Retreived Successfully");

        }

        [HttpGet("keyword{keyword}")]

        public async Task<APIResponse<IEnumerable<CountryResponseDto>>> Search(string keyword)
        {

            try
            {
                var countries = await _service.Search(keyword);

                if (!countries.Any())
                    return new APIResponse<IEnumerable<CountryResponseDto>>(HttpStatusCode.NoContent, "No Countries Found");


                return new APIResponse<IEnumerable<CountryResponseDto>>(countries, "Retreived Successfully");

            }
            catch (Exception ex) 
            {

                return new APIResponse<IEnumerable<CountryResponseDto>>(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

















    }
}