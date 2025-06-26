using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Application.Dtos;
using System.Diagnostics.Metrics;
using Application.Interfaces.Services;

namespace ProjectApproval.ConsoleInteractions.Inputs
{
    public class ConsoleInput
    {
        private readonly IAreaService _areaService;
        private readonly IProjectTypeService _projectTypeService;

        public ConsoleInput(IAreaService areaService, IProjectTypeService projectTypeService)
        {
            _areaService = areaService;
            _projectTypeService = projectTypeService;
        }

        public ProjectProposalDto SolicitarProyecto(UserDto selectedUser)
        {
            Console.Clear();
            Console.WriteLine("===========================================");
            Console.WriteLine($"       CREANDO NUEVO PROYECTO             ");
            Console.WriteLine("===========================================");

            var proyecto = new ProjectProposalDto();
            var id = Guid.NewGuid();

            proyecto.Id = id;
            proyecto.Title = ReadText("Título");
            proyecto.Description = ReadText("Descripción");
            proyecto.EstimatedAmount = ReadDecimal("Monto estimado en pesos");
            proyecto.EstimatedDuration = ReadDecimal("Duración estimada en horas");
            proyecto.CreateAt = DateTime.Now;

            Console.WriteLine("");
            Console.WriteLine("===========================================");
            Console.WriteLine("=============AREAS DISPONIBLES=============");
            Console.WriteLine("===========================================");
            Console.WriteLine("");
            var areas = _areaService.GetAll();
            foreach ( var area in areas )
            {
                  Console.WriteLine("Nombre: "+ area.Name + " | Id: " + area.Id); 
            }
            Console.WriteLine("");
            proyecto.Area = ReadInt("El id del área");

            Console.WriteLine("");
            Console.WriteLine("===========================================");
            Console.WriteLine("=============TIPOS DISPONIBLES=============");
            Console.WriteLine("===========================================");
            Console.WriteLine("");
            var types = _projectTypeService.GetAll();
            foreach (var type in types)
            {
                Console.WriteLine("Nombre: " + type.Name + " | Id: " + type.Id);
            }
            Console.WriteLine("");
            proyecto.Type = ReadInt("el id del tipo");
            proyecto.Status = 1;

            proyecto.CreateBy = selectedUser.Id;

            return proyecto;
        }

        public static string ReadText(string campo)
        {
            string input;
            do
            {
                Console.Write($"Ingrese {campo}: ");
                input = Console.ReadLine()!;
                if (string.IsNullOrWhiteSpace(input))
                    Console.WriteLine($"{campo} no puede estar vacío.");
            } while (string.IsNullOrWhiteSpace(input));

            return input;
        }

        public static int ReadDecimal(string campo)
        {
            int valor;
            string? input;
            do
            {
                Console.Write($"Ingrese {campo}: ");
                input = Console.ReadLine();
            } while (!int.TryParse(input, out valor) || valor <= 0);

            return valor;
        }

        public static int ReadInt(string campo)
        {
            int valor;
            string? input;
            do
            {
                Console.Write($"Ingrese {campo}: ");
                input = Console.ReadLine();
            } while (!int.TryParse(input, out valor) || valor < 0);

            return valor;
        }

        public static DateTime ReadDate(string campo)
        {
            DateTime fecha;
            string? input;
            do
            {
                Console.Write($"Ingrese {campo}: ");
                input = Console.ReadLine();
            } while (!DateTime.TryParse(input, out fecha));

            return fecha;
        }
    }
}

