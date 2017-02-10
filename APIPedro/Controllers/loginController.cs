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
using APIPedro.Models;
using APIPedro.DTO;
using AutoMapper;

namespace APIPedro.Controllers
{
    [RoutePrefix("api/login")]
    public class loginController : ApiController
    {
        private dbunicesarEntities1 db = new dbunicesarEntities1();

        // GET: api/login
        public IQueryable<login> Getlogin()
        {
            return db.login;
        }

        // GET: api/login/5
        [ResponseType(typeof(login))]
        public IHttpActionResult Getlogin(string id)
        {
            login login = db.login.Find(id);
            if (login == null)
            {
                return NotFound();
            }

            return Ok(login);
        }

        // PUT: api/login/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putlogin(string id, login login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != login.Usuario)
            {
                return BadRequest();
            }

            db.Entry(login).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!loginExists(id))
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

        // POST: api/login
        [Route("")]
        [HttpPost]
        [ResponseType(typeof(loginDTO))]
        public IHttpActionResult Postlogin(loginDTO login)
        {
            login log = db.login.Find(login.Usuario);
           
           
            if (log != null)
            {
                if (log.Usuario == login.Usuario && log.Password == login.Password)
                {
                    estudiantes estu = db.estudiantes.Where(t => t.Login_Usuario == log.Usuario).FirstOrDefault();
                    if (estu == null)
                    {
                        profesores pro = db.profesores.Where(t => t.Login_Usuario == log.Usuario).FirstOrDefault();
                        if (pro != null)
                        {
                            HorarioProfDTO prof = new HorarioProfDTO();
                            profesoresDTO pr = new profesoresDTO();
                            Mapper.CreateMap<profesores, profesoresDTO>();
                            Mapper.Map(pro, pr);
                            List<AsignaturaProDTO> lista = new List<AsignaturaProDTO>();
                            List<estudiantesListaDTO> listaE = new List<estudiantesListaDTO>();

                            Mapper.CreateMap<asignaturas, AsignaturaProDTO>();
                            Mapper.Map(db.asignaturas.Where(t => t.Profesores_CeduProf == pr.CeduProf).ToList(), lista);


                            foreach (AsignaturaProDTO Asp in lista) {
                                List<estudiantes> ls = db.asignaturas.Where(t => t.CodiAsig == Asp.CodiAsig && t.Grupo==Asp.Grupo).SelectMany(b=>b.calificaciones.Select(p=>p.estudiantes)) .ToList();
                                Mapper.CreateMap<estudiantes, estudiantesListaDTO>();
                                Mapper.Map(ls, listaE);
                                
                                Asp.Listado = listaE;
                               
                            }
                            foreach (estudiantesListaDTO edt in listaE){
                                edt.Nota1 = 3;
                                edt.Nota2 = 3;
                                edt.Nota3 = 3;
                                edt.Habilitacion = 3;
                            }
                                
                            prof.profesor = pr;
                            prof.materias = lista;
                            return Ok(prof);
                        }
                    }

                    else {
                        HorarioDTO est = new HorarioDTO();
                        estudiantesDTO e = new estudiantesDTO();
                        Mapper.CreateMap<estudiantes, estudiantesDTO>();
                        Mapper.Map(estu, e);
                        est.estudiante = e;
                        List<AsignaturaEstDTO> lista = new List<AsignaturaEstDTO>();

                        Mapper.CreateMap<asignaturas, AsignaturaEstDTO>();
                        Mapper.Map(db.calificaciones.Where(t => t.Estudiantes_CeduEstu == estu.CeduEstu).Select(k => k.asignaturas).ToList(), lista);

                        List<calificaciones> cl = db.calificaciones.Where(t => t.Estudiantes_CeduEstu == estu.CeduEstu).ToList();
                        for (int i = 0; i < cl.Count; i++) {
                            lista[i].Nota1 = cl[i].Nota1;
                            lista[i].Nota2 = cl[i].Nota2;
                            lista[i].Nota3 = cl[i].Nota3;
                            lista[i].Habilitacion = cl[i].Habilitacion;

                        }

                            est.materias = lista;

                        return Ok(est);
                    }
                }
                else {
                    return Ok("Contraseña incorrecta");
                }
            }
            else {
                return Ok("Usuario no registrado");
            }

           

            return CreatedAtRoute("DefaultApi", new { id = login.Usuario }, login);
        }

        // DELETE: api/login/5
        [ResponseType(typeof(login))]
        public IHttpActionResult Deletelogin(string id)
        {
            login login = db.login.Find(id);
            if (login == null)
            {
                return NotFound();
            }

            db.login.Remove(login);
            db.SaveChanges();

            return Ok(login);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool loginExists(string id)
        {
            return db.login.Count(e => e.Usuario == id) > 0;
        }
    }
}