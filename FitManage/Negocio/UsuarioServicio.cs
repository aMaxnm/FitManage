using AccesoDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class UsuarioServicio
    {
        private UsuarioDAO usuarioDAO = new UsuarioDAO();
        public bool VerificarUsuario(string usuario, string contraseña)
        {
            return usuarioDAO.VerificarUsuario(usuario,contraseña);
        }
    }
}
