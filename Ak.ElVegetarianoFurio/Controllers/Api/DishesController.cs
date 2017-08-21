using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Ak.ElVegetarianoFurio.Models;

namespace Ak.ElVegetarianoFurio.Controllers.Api
{
    [Authorize]
    public class DishesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Liefert eine Liste aller Gerichte
        /// </summary>
        /// <returns>Liste der Gerichte</returns>
        [AllowAnonymous]
        // GET: api/Dishes
        public IQueryable<Dish> GetDishes()
        {
            return db.Dishes;
        }

        /// <summary>
        /// Liefert ein Gericht
        /// </summary>
        /// <param name="id">Die Id des Gerichts</param>
        /// <returns>Das Gericht passend zur Id</returns>
        [AllowAnonymous]
        // GET: api/Dishes/5
        [ResponseType(typeof(Dish))]
        public IHttpActionResult GetDish(int id)
        {
            Dish dish = db.Dishes.Find(id);
            if (dish == null)
            {
                return NotFound();
            }

            return Ok(dish);
        }

        /// <summary>
        /// Speichert Änderungen an einem Gericht
        /// </summary>
        /// <param name="id">Id des Gericht</param>
        /// <param name="dish">Das Gericht</param>
        /// <returns>Das geänderte Gericht</returns>
        // PUT: api/Dishes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDish(int id, Dish dish)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dish.Id)
            {
                return BadRequest();
            }

            db.Entry(dish).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DishExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Erstellt ein neues Gericht
        /// </summary>
        /// <param name="dish">Das neue Gericht</param>
        /// <returns>Das erstellte Gericht</returns>
        // POST: api/Dishes
        [ResponseType(typeof(Dish))]
        public IHttpActionResult PostDish(Dish dish)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Dishes.Add(dish);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = dish.Id }, dish);
        }

        /// <summary>
        /// Löscht ein Gericht
        /// </summary>
        /// <param name="id">Nr des Gerichts</param>
        /// <returns>Das gelöschte Gericht</returns>
        // DELETE: api/Dishes/5
        [ResponseType(typeof(Dish))]
        public IHttpActionResult DeleteDish(int id)
        {
            Dish dish = db.Dishes.Find(id);
            if (dish == null)
            {
                return NotFound();
            }

            db.Dishes.Remove(dish);
            db.SaveChanges();

            return Ok(dish);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DishExists(int id)
        {
            return db.Dishes.Count(e => e.Id == id) > 0;
        }
    }
}