using Microsoft.AspNetCore.Mvc;
using Scarpe___Co_.Models;

namespace Scarpe___Co_.Controllers
{
    public class ArticoliController : Controller
    {
        public IActionResult Index()
        {
            var prodotti = ListaArticoli.Prodotti;
            return View(prodotti);
        }

        // Visualizza dettagli
        public IActionResult Dettagli(int id)
        {
            var prodotto = ListaArticoli.Prodotti.FirstOrDefault(p => p.Id == id);
            if (prodotto == null)
            {
                return NotFound();
            }
            return View(prodotto);
        }

        // Aggiungi art alla lista
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Articolo articolo)
        {
            if (ModelState.IsValid)
            {
                // Genera nuovo Id
                articolo.Id = ListaArticoli.Prodotti.Any() ? ListaArticoli.Prodotti.Max(p => p.Id) + 1 : 1;
                ListaArticoli.Prodotti.Add(articolo);
                return RedirectToAction(nameof(Index));
            }
            return View(articolo);
        }

        //Modifica
        public IActionResult Edit(int id)
        {
            var prodotto = ListaArticoli.Prodotti.FirstOrDefault(p => p.Id == id);
            if (prodotto == null)
            {
                return NotFound();
            }
            return View(prodotto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Articolo articolo)
        {
            if (id != articolo.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                var artInLista = ListaArticoli.Prodotti.FirstOrDefault(p => p.Id == id);
                if (artInLista == null)
                {
                    return NotFound();
                }
                artInLista.Nome = articolo.Nome;
                artInLista.Prezzo = articolo.Prezzo;
                artInLista.Descrizione = articolo.Descrizione;
                artInLista.ImmagineCopertina = articolo.ImmagineCopertina;
                artInLista.ImmagineAggiuntiva1 = articolo.ImmagineAggiuntiva1;
                artInLista.ImmagineAggiuntiva2 = articolo.ImmagineAggiuntiva2;
                return RedirectToAction(nameof(Index));
            }
            return View(articolo);
        }

        // Elimina
        public IActionResult Delete(int id)
        {
            var prodotto = ListaArticoli.Prodotti.FirstOrDefault(p => p.Id == id);
            if (prodotto == null)
            {
                return NotFound();
            }
            return View(prodotto);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var prodotto = ListaArticoli.Prodotti.FirstOrDefault(p => p.Id == id);
            if (prodotto == null)
            {
                return NotFound();
            }
            ListaArticoli.Prodotti.Remove(prodotto);
            return RedirectToAction(nameof(Index));
        }
    }

}
