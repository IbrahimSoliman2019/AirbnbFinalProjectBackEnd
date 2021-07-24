using System.Collections.Generic;
using System.Threading.Tasks;
using Api.DTOS;
using AutoMapper;
using Domain.Entities;
using Domain.EntitiesSpecification.Bookingspec;
using Domain.EntitiesSpecification.TransactionSpec;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class ReservationController : ApiBaseController
    {
        private readonly IGenericRepo<Booking> _bookingrepo;

        private readonly IGenericRepo<transaction> _transactionrepo;

        private readonly IMapper _mapper;

        public ReservationController(
            IGenericRepo<Booking> bookingrepo,
            IGenericRepo<transaction> transactionrepo,
            IMapper mapper
        )
        {
            _mapper = mapper;
            _transactionrepo = transactionrepo;
            _bookingrepo = bookingrepo;
        }

        [HttpGet("bookings/{id}")]
        public async Task<ActionResult<List<BookingDTO>>> GetBookings(int id)
        {
            var spec = new BookingSpecification(id);
            var bookings = await _bookingrepo.ListAllBySpec(spec);
            var mapped =
                _mapper.Map<IReadOnlyList<Booking>, List<BookingDTO>>(bookings);
            return Ok(mapped);
        }

        [HttpGet("transaction/{id}")]
        public async Task<ActionResult<List<TransactionDto>>>
        Gettransactions(int id)
        {
            var spec = new TransactionSpecification(id);
            var transactions = await _transactionrepo.ListAllBySpec(spec);
            var mapped =
                _mapper
                    .Map
                    <IReadOnlyList<transaction>, List<TransactionDto>
                    >(transactions);
            return Ok(mapped);
        }

        [HttpPost("booking")]
        public async Task<ActionResult<BookingDTO>>
        PostBooking(BookingDTO booking)
        {
            var Booking = _mapper.Map<BookingDTO, Booking>(booking);
            var obj = await _bookingrepo.AddAsync(Booking);
            return Ok(_mapper.Map<Booking, BookingDTO>(obj));
        }
         [HttpPost("transaction")]
        public async Task<ActionResult<TransactionDto>>
        PostTransaction(TransactionDto transaction)
        {
            var Transaction = _mapper.Map<TransactionDto, transaction>(transaction);
            var obj = await _transactionrepo.AddAsync(Transaction);
            return Ok(_mapper.Map<transaction, TransactionDto>(obj));
        }
    }
}
