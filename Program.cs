using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static System.Console;


public class InstitutoContext : DbContext
{

    public DbSet<Alumno> Alumnos { get; set; }

    public DbSet<Modulo> Modulos { get; set; }

    public DbSet<Matricula> Matriculas { get; set; }

    public string connString { get; private set; }

    public InstitutoContext()
    {
        var database = "EF06Andoitz"; // "EF{XX}Nombre" => EF00Santi
        connString = $"Server=185.60.40.210\\SQLEXPRESS,58015;Database={database};User Id=sa;Password=Pa88word;MultipleActiveResultSets=true";
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Matricula>().HasIndex(m => new
        {
            m.AlumnoId,
            m.ModuloId
        }).IsUnique();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer(connString);

}
public class Alumno
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int AlumnoId { get; set; }

    public string Nombre { get; set; }

    public int Edad { get; set; }

    public decimal Efectivo { get; set; }

    public string Pelo { get; set; }

    public override string ToString() => $"El alumno, {Nombre}, cuyo id es: {AlumnoId}, tiene una edad de {Edad} años y color de pelo {Pelo}";

}
public class Modulo
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int ModuloId { get; set; }

    public string Titulo { get; set; }

    public int Creditos { get; set; }

    public int Curso { get; set; }

    public override string ToString() => $"El título: {Titulo}, con id: {ModuloId}, tiene {Creditos} créditos y se cursa en {Curso} curso";

}
public class Matricula
{

    [Key]
    public int MatriculaId { get; set; }

    public int AlumnoId { get; set; }

    public int ModuloId { get; set; }

    public Alumno Alumno { get; set; }
    public Modulo Modulo { get; set; }

    public override string ToString() => $"La matrícula: {MatriculaId} pertenece al alumno con Id: {AlumnoId}, en el siguiente módulo {ModuloId}";

}

class Program
{
    static void GenerarDatos()
    {
        using (var db = new InstitutoContext())
        {
            // Borrar todo
            db.Matriculas.RemoveRange(db.Matriculas);
            db.Alumnos.RemoveRange(db.Alumnos);
            db.Modulos.RemoveRange(db.Modulos);

            // Añadir Alumnos
            // Id de 1 a 7

            db.Add(new Alumno { AlumnoId = 1, Nombre = "Paco Manteca", Edad = 29, Efectivo = 5000M, Pelo = "moreno" });
            db.Add(new Alumno { AlumnoId = 2, Nombre = "Carmen de Mairena", Edad = 87, Efectivo = 5000M, Pelo = "castaño" });
            db.Add(new Alumno { AlumnoId = 3, Nombre = "Joselito", Edad = 32, Efectivo = 5000M, Pelo = "rubio" });
            db.Add(new Alumno { AlumnoId = 4, Nombre = "Don Comedia", Edad = 51, Efectivo = 5000M, Pelo = "moreno" });
            db.Add(new Alumno { AlumnoId = 5, Nombre = "DCOPN", Edad = 33, Efectivo = 5000M, Pelo = "rubio" });
            db.Add(new Alumno { AlumnoId = 6, Nombre = "MariaDB", Edad = 12, Efectivo = 5000M, Pelo = "castaño" });
            db.Add(new Alumno { AlumnoId = 7, Nombre = "Bisbalisimo", Edad = 42, Efectivo = 5000M, Pelo = "moreno" });
            db.SaveChanges();

            // Añadir Módulos
            // Id de 1 a 10

            db.Add(new Modulo { ModuloId = 1, Titulo = "Sexación de Pollos", Creditos = 2, Curso = 2 });
            db.Add(new Modulo { ModuloId = 2, Titulo = "Inspección de Memes", Creditos = 5, Curso = 1 });
            db.Add(new Modulo { ModuloId = 3, Titulo = "Cuidar de Pingüinos", Creditos = 8, Curso = 2 });
            db.Add(new Modulo { ModuloId = 4, Titulo = "Encender de Hogueras", Creditos = 1, Curso = 1 });
            db.Add(new Modulo { ModuloId = 5, Titulo = "Moda refacherita", Creditos = 3, Curso = 1 });
            db.Add(new Modulo { ModuloId = 6, Titulo = "Encantar sapos", Creditos = 3, Curso = 2 });
            db.Add(new Modulo { ModuloId = 7, Titulo = "Alicatar Baños", Creditos = 1, Curso = 2 });
            db.Add(new Modulo { ModuloId = 8, Titulo = "Invertir en Criptomonedas", Creditos = 5, Curso = 1 });
            db.Add(new Modulo { ModuloId = 9, Titulo = "Investigación de Injertos de Pelo", Creditos = 4, Curso = 1 });
            db.Add(new Modulo { ModuloId = 10, Titulo = "Canticos Gregorianos", Creditos = 2, Curso = 2 });
            db.SaveChanges();

            // Matricular Alumnos en Módulos

            foreach (var m in db.Modulos)
            {
                foreach (var a in db.Alumnos)
                {
                    db.Add(new Matricula
                    {
                        AlumnoId = a.AlumnoId,
                        ModuloId = m.ModuloId

                    });
                }
            }
            db.SaveChanges();

        }
    }

    static void BorrarMatriculaciones()
    {
        using (var db = new InstitutoContext())
        {

            // Borrar las matriculas 

            foreach (var m in db.Matriculas)
            {

                // de AlumnoId multiplo de 3 y ModuloId Multiplo de 2;

                if (m.AlumnoId % 3 == 0 && m.ModuloId % 2 == 0)
                {
                    db.Matriculas.Remove(m);
                }

                //// de AlumnoId multiplo de 2 y ModuloId Multiplo de 5;

                if (m.AlumnoId % 2 == 0 && m.ModuloId % 5 == 0)
                {
                    db.Matriculas.Remove(m);
                }
            }

            db.SaveChanges();

        }

    }
    static void RealizarQuery()
    {
        using (var db = new InstitutoContext())
        {
            // Las queries que se piden en el examen

            // Filtering(cada 1)

            Console.WriteLine("\nQuery 1-->\n");

            var query1 = db.Matriculas.Where(o => o.AlumnoId == 1);

            foreach (var item in query1)
            {
                Console.WriteLine(item.ToString());
            }

            //Anomnimous tupe(cada 1)

            Console.WriteLine("\nQuery 2-->\n");

            var query2 = db.Alumnos.Select(o => new
            {
                Name = o.Nombre,
                saldo = o.Efectivo
            }
            );

            foreach (var item in query2)
            {
                Console.WriteLine(item.ToString());
            }


            //Ordenring(cada 1)

            Console.WriteLine("\nQuery 3-->\n");

            var query3 = db.Modulos.OrderBy(o => o.Creditos);

            foreach (var item in query3)
            {
                Console.WriteLine(item.ToString());
            }

            Console.WriteLine("\nQuery 4-->\n");

            var query4 = db.Modulos.OrderByDescending(o => o.Creditos);

            foreach (var item in query4)
            {
                Console.WriteLine(item.ToString());
            }

            Console.WriteLine("\nQuery 5-->\n");

            var query5 = db.Modulos.OrderBy(o => o.Creditos).
                                    ThenByDescending(o => o.Curso);


            foreach (var item in query5)
            {
                Console.WriteLine(item.ToString());
            }

            //Joining

            Console.WriteLine("\nQuery 6-->\n");

            var query6 = db.Alumnos.Join(db.Matriculas,
                c => c.AlumnoId, o => o.AlumnoId,
                (c, o) => new
                {
                    c.AlumnoId,
                    c.Nombre,
                    o.MatriculaId,
                }
                    );

            foreach (var item in query6)
            {
                Console.WriteLine(item.ToString());
            }

            //Grouping

            Console.WriteLine("\nQuery 7-->\n");

            var query7 = db.Matriculas.GroupBy(
            o => o.AlumnoId).
            Select(g => new
            {
                AlumnoId = g.Key,
                Matriculas = g.Count()
            });

            foreach (var item in query7)
            {
                Console.WriteLine(item.ToString());
            }

            //Paging(cada 1)

            Console.WriteLine("\nQuery 8-->\n");

            var query8 = db.Matriculas.Where(
                o => o.AlumnoId == 1
            ).Take(3);

            foreach (var item in query8)
            {
                Console.WriteLine(item.ToString());
            }

            Console.WriteLine("\nQuery 9-->\n");

            var query9 = (from o in db.Matriculas
                          where o.AlumnoId == 1
                          orderby o.MatriculaId
                          select o).Skip(2).Take(2);

            foreach (var item in query9)
            {
                Console.WriteLine(item.ToString());
            }

            //Elements Opertators(cada 1)

            Console.WriteLine("\nQuery 10-->\n");

            var query10 = db.Alumnos.Single(
            c => c.AlumnoId == 1);

            Console.WriteLine(query10.ToString());

            Console.WriteLine("\nQuery 11-->\n");

            var query11 = db.Alumnos.SingleOrDefault(
            c => c.AlumnoId == 145);

            if (query11 == null)
            {
                Console.WriteLine("null");
            }
            else
            {
                Console.WriteLine(query11);
            }

            Console.WriteLine("\nQuery 12-->\n");

            var query12 = db.Alumnos.Where(
            c => c.AlumnoId == 145
            ).ToList().DefaultIfEmpty(new Alumno()).Single();

            Console.WriteLine(query12.ToString());

            Console.WriteLine("\nQuery 13-->\n");

            var query13 = db.Alumnos.Where(
            o => o.AlumnoId == 1).
            OrderBy(o => o.Edad).Last();

            Console.WriteLine(query13.ToString());

            Console.WriteLine("\nQuery 14-->\n");

            var query14 = db.Alumnos.Where(
            c => c.AlumnoId == 1).
            Select(o => o.AlumnoId).SingleOrDefault();

            Console.WriteLine(query14.ToString());

            Console.WriteLine("\nQuery 15-->\n");

            var query15 = db.Alumnos.Where(
                c => c.AlumnoId == 484).
            Select(o => o.AlumnoId).SingleOrDefault();

            Console.WriteLine(query15.ToString());

            //Conversiones:

            //ToArray

            Console.WriteLine("\nQuery 16-->\n");

            string[] nombres = (from c in db.Alumnos
                                select c.Nombre).ToArray();

            foreach (var item in nombres)
            {
                Console.WriteLine(item.ToString());
            }


            //ToDicctionary

            Console.WriteLine("\nQuery 17-->\n");

            Dictionary<int, Alumno> diccionario = db.Alumnos.ToDictionary(c => c.AlumnoId);

            foreach (var item in diccionario)
            {
                Console.WriteLine(item.ToString());
            }

            //ToList

            Console.WriteLine("\nQuery 18-->\n");

            List<Modulo> listisima = (from o in db.Modulos
                                      where o.Creditos >= 4
                                      orderby o.Curso
                                      select o).ToList();

            foreach (var item in listisima)
            {
                Console.WriteLine(item.ToString());
            }

            //ILookup

            Console.WriteLine("\nQuery 19-->\n");

            ILookup<int, string> lookup =
            db.Alumnos.ToLookup(c => c.AlumnoId, c => c.Nombre);

            foreach (var item in lookup)
            {
                foreach (var listilaInterna in item)
                {
                    Console.WriteLine(listilaInterna.ToString());
                }
            }

        }
    }

    static void Main(string[] args)
    {
        GenerarDatos();
        BorrarMatriculaciones();
        RealizarQuery();
    }

}