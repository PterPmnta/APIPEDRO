using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIPedro.DTO
{
    public  class loginDTO
    {
        public string Usuario { get; set; }
        public string Password { get; set; }
       
    }
    public  class profesoresDTO
    {
        public string CeduProf { get; set; }
        public string NombProf { get; set; }
        public string ApelProf { get; set; }
        public int RolProf { get; set; }
        public string FacuProf { get; set; }
        public string ProgProf { get; set; }
        public string Login_Usuario { get; set; }

    }
    public  class estudiantesDTO
    {
       
        public string CeduEstu { get; set; }
        public string NombEstu { get; set; }
        public string ApelEstu { get; set; }
        public int RolEstu { get; set; }
        public string FacuEstu { get; set; }
        public string ProgEstu { get; set; }
        public int Semestre { get; set; }
        public string Login_Usuario { get; set; }

    }

    public class estudiantesListaDTO
    {

        public string CeduEstu { get; set; }
        public string NombEstu { get; set; }
        public string ApelEstu { get; set; }
        public Nullable<float> Nota1 { get; set; }
        public Nullable<float> Nota2 { get; set; }
        public Nullable<float> Nota3 { get; set; }
        public Nullable<float> Habilitacion { get; set; }
    }

    public class AsignaturaProDTO
    {
        public string CodiAsig { get; set; }
        public int Grupo { get; set; }
        public string NombAsig { get; set; }
        public Nullable<int> Creditos { get; set; }
        public string DiaAsig { get; set; }
        public string HoraAsig { get; set; }
        public string LugarAsig { get; set; }

        public List<estudiantesListaDTO> Listado { get; set; }
      
    }

    public class AsignaturaEstDTO
    {
        public string CodiAsig { get; set; }
        public int Grupo { get; set; }
        public string NombAsig { get; set; }
        public Nullable<int> Creditos { get; set; }
        public string DiaAsig { get; set; }
        public string HoraAsig { get; set; }
        public string LugarAsig { get; set; }
        public string Profesores_CeduProf { get; set; }
        public Nullable<float> Nota1 { get; set; }
        public Nullable<float> Nota2 { get; set; }
        public Nullable<float> Nota3 { get; set; }
        public Nullable<float> Habilitacion { get; set; }
    }
    public class HorarioDTO {
        public estudiantesDTO estudiante { get; set; }
        public List<AsignaturaEstDTO> materias { get; set; }
    }
    public class HorarioProfDTO
    {
        public profesoresDTO profesor { get; set; }
        public List<AsignaturaProDTO> materias { get; set; }
    }

}