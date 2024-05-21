﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VistaEntidad;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace VistaDatos
{
    public class D_Usuarios
    {
        public List<UsuarioCerezos> listar()
        {
            List<UsuarioCerezos> lista = new List<UsuarioCerezos>();

            try
            {
                using (SqlConnection oconecion = new SqlConnection(Conexion.cn))
                {
                    //Consultar a la bd
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("SELECT u.IDUsuario, u.Nombre, u.Apellido, u.Email, u.Clave, u.Restablecer, u.FechaRegistro, u.Activo,");
                    sb.AppendLine("r.IDRol, r.NombreRol");
                    sb.AppendLine("from Usuario u");
                    sb.AppendLine("inner join Rol r on r.IDRol = u.IDRol");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), oconecion);
                    cmd.CommandType = CommandType.Text;
                    oconecion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        //Almacenar cada registro en la lista
                        while (dr.Read())
                        {
                            lista.Add(
                                new UsuarioCerezos()
                                {
                                    IDUsuario = Convert.ToInt32(dr["IDUsuario"]),
                                    Nombre = dr["Nombre"].ToString(),
                                    Apellido = dr["Apellido"].ToString(),
                                    Email = dr["Email"].ToString(),
                                    Clave = dr["Clave"].ToString(),
                                    Restablecer = Convert.ToBoolean(dr["Restablecer"]),
                                    FechaRegistro = dr["FechaRegistro"].ToString(),
                                    Activo = Convert.ToBoolean(dr["Activo"]),
                                    oRol = new RolCerezos() {IDRol = Convert.ToInt32(dr["IDRol"]), NombreRol = dr["NombreRol"].ToString() }
                                
                                }
                                );
                        }
                    }
                }
            }
            catch
            {
                lista = new List<UsuarioCerezos>();
            }

            return lista;
        }
    }
}
