﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace APINetCore.Models;

public partial class Imex_Terminal
{
    public int TerminalId { get; set; }

    public string Descripcion { get; set; }

    public string Codigo { get; set; }

    public string Splc { get; set; }

    public bool? Activo { get; set; }

    public bool? Interpacifico { get; set; }

    public int? horas { get; set; }

    public int? horaDiferencia { get; set; }

    public string EmailOperaciones { get; set; }

    public bool? AceptaCambioPatio { get; set; }

    public bool GeneraPedido { get; set; }

    public int? Maritimo { get; set; }

    public bool? ATR { get; set; }

    public bool? Intermodal { get; set; }

    public decimal? Latitud { get; set; }

    public decimal? Longitud { get; set; }

    public string RutaServerLocal { get; set; }

    public bool? Remolques { get; set; }

    public bool? Trasvase { get; set; }

    public string SegmentoRed { get; set; }

    public bool? SIMEXPRO { get; set; }

    public bool? SIMEX_API { get; set; }

    public int? MaxContainers { get; set; }

    public int? TiempoRecoleccion { get; set; }

    public int? Almacenaje { get; set; }

    public bool? Sidopla { get; set; }

    public bool SIDOPLAFiltradoCrossBorderSPLC { get; set; }

    public bool CargaMasivaSidopla { get; set; }

    public bool ValidaSoloContenedoresSidopla { get; set; }

    public bool? EnviaSAPMyR { get; set; }

    public bool? ConsultaMaterialesSAPMyR { get; set; }

    public bool? Recarga { get; set; }
}