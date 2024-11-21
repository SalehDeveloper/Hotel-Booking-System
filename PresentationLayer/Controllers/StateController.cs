using BLL.DTOs;
using BLL.Services;
using DAC.Data;
using DAC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController (IStateService _service , AppDbContext _context): ControllerBase
    {


        [HttpPost]
        public async Task<APIResponse<StateResponseDto>> AddNewState([FromBody] StateRequestDto dto)
        {

            if (!ModelState.IsValid)
            {
                return new APIResponse<StateResponseDto>(HttpStatusCode.BadRequest, "Invalid inputs");
            }

            try
            {
                var res = await _service.AddAsync(dto);

                if (res is not null)

                    return new APIResponse<StateResponseDto>(res, "New State Added Successfully");

                return new APIResponse<StateResponseDto>(HttpStatusCode.BadRequest, "Failed To Add");

            }
            catch (Exception ex)
            {

                return new APIResponse<StateResponseDto>(HttpStatusCode.InternalServerError, ex.Message);
            }

          
        }



        [HttpPut("{id}")]
        public async Task<APIResponse<StateResponseDto>> Update(int id, [FromBody] StateRequestDto dto)
        {

            if (!ModelState.IsValid)
                return new APIResponse<StateResponseDto>(HttpStatusCode.BadRequest, "Invalid inputs");

            try
            {
                var res = await _service.UpdateAsync(id, dto);

                if (res is not null)
                {


                    return new APIResponse<StateResponseDto>(res, "Updated Successfully");

                }

                return new APIResponse<StateResponseDto>(HttpStatusCode.NotFound, $"Failed To Updated");
            }
            catch (Exception ex)
            {
                return new APIResponse<StateResponseDto>(HttpStatusCode.InternalServerError, ex.Message);

            }



        }

      
        
        
        [HttpDelete("{id}")]
        public async Task<APIResponse<int>> Delete(int id)
        {

            var StateToDelete = await _context.States.FindAsync(id);

            if (StateToDelete != null)
            {
                if (!StateToDelete.IsActive)
                    return new APIResponse<int>(-1, $"State with id {id} Already Deleted ", HttpStatusCode.NoContent);
            }

            try
            {
                var res = await _service.DeleteAsync(id);

                if (res)
                    return new APIResponse<int>(id, "Deleted Successfully");

                return new APIResponse<int>(-1, $"State with id {id} Not Found", HttpStatusCode.BadRequest);
            }

            catch (Exception ex)
            {

                return new APIResponse<int>(-1, ex.Message, HttpStatusCode.InternalServerError);

            }


        }

        
        
        [HttpGet("id/{id}")]
        public async Task<APIResponse<StateResponseDto>> GetByID(int id)
        {
            try
            {
                var res = await _service.GetById(id);


                if (res is not null)
                {
                    return new APIResponse<StateResponseDto>(res, "State Retreived Successfully");

                }
            }
            catch (Exception ex)
            {

                return new APIResponse<StateResponseDto>(HttpStatusCode.InternalServerError, ex.Message);


            }

            return new APIResponse<StateResponseDto>(HttpStatusCode.NotFound, $"State With Id {id} Not found");





        }


       
        
        [HttpGet("name/{name}")]
        public async Task<APIResponse<StateResponseDto>> GetByName(string name)
        {


            try
            {

                var res = await _service.GetByNameAsync(name);



                if (res is not null)

                {


                    return new APIResponse<StateResponseDto>(res, "Country Retreived Successfully");

                }
            }
            catch (Exception ex)
            {

                return new APIResponse<StateResponseDto>(HttpStatusCode.InternalServerError, ex.Message);

            }

            return new APIResponse<StateResponseDto>(HttpStatusCode.NotFound, $"Country With name {name} Not found");
        }

        [HttpGet]
        [Route("Active")]
        public async Task<APIResponse<IEnumerable<StateResponseDto>>> GetAllActive()
        {
            try
            {
                var res = await _service.GetAllActiveAsync();

                if (res.Any())
                {


                    return new APIResponse<IEnumerable<StateResponseDto>>(res, "States Retreived Successfully");

                }

            }
            catch (Exception ex)
            {
                return new APIResponse<IEnumerable<StateResponseDto>>(HttpStatusCode.InternalServerError, ex.Message);


            }




            return new APIResponse<IEnumerable<StateResponseDto>>(HttpStatusCode.NotFound, "No Active States Found");
        }


        [HttpGet]
        [Route("InActive")]
        public async Task<APIResponse<IEnumerable<StateResponseDto>>> GetAllInActive()
        {

            try
            {
                var res = await _service.GetAllInActiveAsync();

                if (res.Any())
                {


                    return new APIResponse<IEnumerable<StateResponseDto>>(res, "States Retreived Successfully");
                }

            }
            catch (Exception ex)
            {


                return new APIResponse<IEnumerable<StateResponseDto>>(HttpStatusCode.InternalServerError, ex.Message);

            }

            return new APIResponse<IEnumerable<StateResponseDto>>(HttpStatusCode.NotFound, "No InActive States Found");
        }


        [HttpGet]
        [Route("All")]
        public async Task<APIResponse<IEnumerable<StateResponseDto>>> GetAll()
        {
            try
            {
                var res = await _service.GetAllAsync();

                if (!res.Any() || res is null)
                    return new APIResponse<IEnumerable<StateResponseDto>>(HttpStatusCode.NoContent, "No States Found");


                return new APIResponse<IEnumerable<StateResponseDto>>(res, "Retreived Successfully");
            }

            catch (Exception ex)
            {

                return new APIResponse<IEnumerable<StateResponseDto>>(HttpStatusCode.InternalServerError, ex.Message);

            }



        }


        [HttpGet("keyword{keyword}")]

        public async Task<APIResponse<IEnumerable<StateResponseDto>>> Search(string keyword)
        {

            try
            {
                var states = await _service.Search(keyword);

                if (!states.Any())
                    return new APIResponse<IEnumerable<StateResponseDto>>(HttpStatusCode.NoContent, "No States Found");


                return new APIResponse<IEnumerable<StateResponseDto>>(states, "Retreived Successfully");

            }
            catch (Exception ex)
            {

                return new APIResponse<IEnumerable<StateResponseDto>>(HttpStatusCode.InternalServerError, ex.Message);
            }

        }


    }
}
