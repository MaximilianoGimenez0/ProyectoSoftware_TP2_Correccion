using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Interfaces;
using Application.Interfaces.Services;
using Domain.Entities;
using ProjectApproval.ConsoleInteractions.Inputs;

namespace ProjectApproval.ConsoleInteractions.Menues
{
    public class Menues
    {
        private readonly IProjectProposalService _projectProposalService;

        public Menues(IProjectProposalService projectProposalService)
        {
            _projectProposalService = projectProposalService;
        }

        public static int MainMenu()
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("         SISTEMA DE PROYECTOS");
            Console.WriteLine("========================================");
            Console.WriteLine("Seleccione la opción que desea ejecutar:\n");
            Console.WriteLine("  1 -> Ver Proyectos");
            Console.WriteLine("  2 -> Agregar Proyecto");
            Console.WriteLine("  3 -> Aprobar o Rechazar un Proyecto");
            Console.WriteLine("  4 -> Ver todas las aprobaciones pendientes");
            Console.WriteLine("  0 -> Salir");
            Console.WriteLine("========================================");

            var opt = ConsoleInput.ReadInt("Ingrese una opción");
            return opt;
        }


        public void ViewProjects() 
        {
            var projects = new List<ProjectProposalViewDto>();
            projects = _projectProposalService.ViewAll();

            int counter = 1;

            foreach (var project in projects)
            {
                Console.WriteLine("===========================================");
                Console.WriteLine($"       Proyecto N° {counter}");
                Console.WriteLine("===========================================");
                Console.WriteLine($" Id:                {project.Id}");
                Console.WriteLine($" Título:            {project.Title}");
                Console.WriteLine($" Descripción:       {project.Description}");
                Console.WriteLine($" Monto Estimado:    {project.EstimatedAmount}");
                Console.WriteLine($" Duración Estimada: {project.EstimatedDuration}");
                Console.WriteLine($" Fecha de Creación: {project.CreateAt}");
                Console.WriteLine($" Área:              {project.Area}");
                Console.WriteLine($" Tipo:              {project.Type}");
                Console.WriteLine($" Estado:            {project.Status}");
                Console.WriteLine($" Creado por:        {project.CreateBy}");
                Console.WriteLine("===========================================\n");

                counter++;
            }

            


        }

    }
}
