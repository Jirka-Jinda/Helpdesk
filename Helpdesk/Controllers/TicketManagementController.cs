using Database;
using Database.Repositories.Abstraction;
using Domain.Ticket;
using Domain.User;
using Domain.Workflow.Enums;
using Microsoft.EntityFrameworkCore;
using System;

namespace Helpdesk.Controllers
{
    public class TicketManagementController : Controller
    {
        private IStorageManager _storageManager;
        private ITicketRepository _ticketRepository;

        public TicketManagementController(IStorageManager storageManager, ITicketRepository ticketRepository)
        {
            _storageManager = storageManager;
            _ticketRepository = ticketRepository;            
        }

        public async Task<IActionResult> Overview(string filterText)
        {
            var allTickets = await _ticketRepository.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(filterText))
            {
                allTickets = allTickets
                    .Where(t => t.Header != null && t.Header.Contains(filterText, StringComparison.OrdinalIgnoreCase))
                    .ToList();
                ViewBag.FilterText = filterText;
            }

            return View(allTickets);
        }

        [Navigation(NavigationName = "Detail")]
        public async Task<IActionResult> Detail(Guid ticketId)
        {
            var ticket = await _ticketRepository.GetAsync(ticketId);
            return View(ticket);
        }

        [Navigation(IgnoreMove = true)]
        public async Task<IActionResult> PostMessage(Guid ticketId, string content)
        {
            var ticket = await _ticketRepository.GetAsync(ticketId);
            if (ticket is not null)
            {
                ticket.AddMessage(content);
                var updatedTicket = await _ticketRepository.UpdateAsync(ticket); 
                return View("Detail", updatedTicket);
            }
            return View("Detail", ticket);
        }

        [Navigation(IgnoreMove = true)]
        public async Task<IActionResult> ChangeWF(Guid ticketId, WFAction action)
        {
            var ticket = await _ticketRepository.GetAsync(ticketId);
            if (ticket is not null)
            {
                ticket.CreateStateTransition(action);
                var updatedTicket = await _ticketRepository.UpdateAsync(ticket);
                return View("Detail", updatedTicket);
            }
            return View("Detail", ticket);
        }

        [Navigation(IgnoreMove = true)]
        public async Task<IActionResult> ChangeSolver(Guid ticketId, User newSolver)
        {
            var ticket = await _ticketRepository.GetAsync(ticketId);
            if (ticket is not null)
            {
                ticket.CreateSolverTransition(newSolver, "", null);
                var updatedTicket = await _ticketRepository.UpdateAsync(ticket);
                return View("Detail", updatedTicket);
            }
            return View("Detail", ticket);
        }
    }
}
