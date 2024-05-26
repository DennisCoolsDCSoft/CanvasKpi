using System;
using System.Threading;
using CanvasKpi.Models;
using CompetenceProfilingDomain.Domain;
using Xunit;
using Xunit.Abstractions;

namespace CanvasKpiLtiTests;

public class CardTests
{
    readonly ITestOutputHelper _output;

    public CardTests(ITestOutputHelper output)
    {
        this._output = output;
    }

    [Fact]
    public void Test1()
    {
        var analysis = new Card("_5759","1", "Analysis-H1.1", "<p><strong>EN</strong><br>Describe the architecture of a computer system.</p> <p><strong>NL</strong><br>Beschrijven van de architectuur van een computersysteem.</p>");
        _output.WriteLine($"{analysis.Description}:{analysis.Architecktuur}:{analysis.Activitiet}:{analysis.Niveaus}:{analysis.LongDescription}");
        var design = new Card("35984_9565","2", "Design-S2.1", "<p><strong>EN<br></strong> Compile a design for a software system while taking into account the use of the existing components and libraries.</p> <p><strong>NL<br></strong> Opstellen van een ontwerp voor een softwaresysteem, rekening houdend met het gebruik van bestaande componenten en libraries.</p>");
        _output.WriteLine($"{design.Description}:{design.Architecktuur}:{design.Activitiet}:{design.Niveaus}:{design.LongDescription}");

        var foo = new Card("35984_6304","3" ,"FOO 3.1", "<p><span>• You analyse the environment and stakeholders of the assignment. </span></p> <p><span>• Je analyseert de omgeving en belanghebbenden van de opdracht.</span></p>");
        _output.WriteLine($"{foo.Description}:{foo.Architecktuur}:{foo.Activitiet}:{foo.Niveaus}:{foo.LongDescription}");
        
        Assert.False(false);
    }
}