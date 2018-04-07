using AutoMapper;
using InLogFrota.ApresentationWeb.Controllers.Base;
using InLogFrota.Core.Services;
using InLogFrota.Core.ViewModels;
using InLogFrota.Data.Entities;
using InLogFrota.Impl.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InLogFrota.ApresentationWeb.Controllers
{
    public class VeiculoController : BaseController
    {
        private readonly IVeiculoService _veiculoService;
        private readonly IMapper _mapper;

        public VeiculoController(IVeiculoService veiculoService, IMapper mapper, INotificationHandler<Notification> notifications) : base(notifications)
        {
            _veiculoService = veiculoService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index() => 
            View(_mapper.Map<IEnumerable<VeiculoViewModel>>(await _veiculoService.GetAllAsync()));

        [HttpGet]
        public IActionResult New() => 
            View();

        [HttpPost]
        public async Task<IActionResult> New(VeiculoViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View();
            await _veiculoService.AddAsync(_mapper.Map<Veiculo>(viewModel));
            return Response("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string chassi) => 
            View(_mapper.Map<VeiculoViewModel>(await _veiculoService.GetByCriteriaAsync(x => x.Chassi == chassi)));

        [HttpPost]
        public async Task<IActionResult> Edit(VeiculoViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View();
            await _veiculoService.UpdateAsync(_mapper.Map<Veiculo>(viewModel));
            return Response("Index");
        }

        [HttpGet]
        public IActionResult Search() => 
            View();

        [HttpPost]
        public async Task<IActionResult> Search(string chassi) => 
            View(_mapper.Map<VeiculoViewModel>(await _veiculoService.GetByCriteriaAsync(x => x.Chassi == chassi)));

        [HttpGet]
        public async Task<IActionResult> SearchByChassi(string chassi)
        {
            var veiculo = _mapper.Map<VeiculoViewModel>(await _veiculoService.GetByCriteriaAsync(x => x.Chassi == chassi));

            if (veiculo == null)
                return Json(new { error = "Veículo não encontrado" });
            return Json(veiculo);
        }

        [HttpGet]
        public async Task<IActionResult> Remove(string chassi) => 
            View(_mapper.Map<VeiculoViewModel>(await _veiculoService.GetByCriteriaAsync(x => x.Chassi == chassi)));

        [HttpPost]
        public async Task<IActionResult> Remove(VeiculoViewModel viewModel)
        {
            await _veiculoService.RemoveAsync(_mapper.Map<Veiculo>(viewModel));
            return RedirectToAction("Index");
        }
    }
}