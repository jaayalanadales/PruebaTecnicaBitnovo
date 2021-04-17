using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PruebaTecnicaBitnovo.Models
{
    /// <summary>
    /// Clase que contiene los datos relativos a un usuario
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// Clave primaria en BBDD
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Nombre de usuario
        /// </summary>
        [MaxLength(50)]
        public string NombreUsuario { get; set; }

        /// <summary>
        /// Contraseña no encriptada
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

        /// <summary>
        /// Nombre
        /// </summary>
        [MaxLength(50)]
        public string Nombre { get; set; }

        /// <summary>
        /// Apellidos
        /// </summary>
        [MaxLength(50)]
        public string Apellidos { get; set; }

        /// <summary>
        /// Balance en euros
        /// </summary>
        [Required]
        public double Balance { get; set; }

        /// <summary>
        /// Determina si el usuario es admin o no
        /// </summary>
        [Required]
        public bool Administrador { get; set; }

    }
}