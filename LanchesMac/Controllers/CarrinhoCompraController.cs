using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using LanchesMac.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace LanchesMac.Controllers
{
    public class CarrinhoCompraController : Controller
    {
        private readonly ILancheRepository _lancheRepository;
        private readonly CarrinhoCompra _carrinhoCompra;
        public CarrinhoCompraController(ILancheRepository lancheRepository, CarrinhoCompra carrinhoCompra)
        {
            _lancheRepository = lancheRepository;
            _carrinhoCompra = carrinhoCompra;   
        }
        
        public IActionResult Index()
        {
            
            var itens = _carrinhoCompra.GetCarrinhoCompraItems();
            _carrinhoCompra.CarrinhoCompraItens = itens;

            var carrinhoCompraVM = new CarrinhoCompraViewModel
            {
                CarrinhoCompra = _carrinhoCompra,
                CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoCompraTotal()
            };

            return View(carrinhoCompraVM);
        }
        public IActionResult AdicionarItemNoCarrinhoCompra(int LancheId)
        {
            var LancheSelecionado = _lancheRepository.Lanches.FirstOrDefault(p => p.LancheId == LancheId);

            if (LancheSelecionado != null)
            {
                _carrinhoCompra.AdicionarAoCarrinho(LancheSelecionado);
            }

            return RedirectToAction("Index");
        }
        public IActionResult RemoverItemDoCarrinhoCompra(int LancheId)
        {
            var LancheSelecionado = _lancheRepository.Lanches.FirstOrDefault(p => p.LancheId == LancheId);

            if (LancheSelecionado != null)
            {
                _carrinhoCompra.RemoverDoCarrinho(LancheSelecionado);
            }

            return RedirectToAction("Index"); 
        }
    }
}