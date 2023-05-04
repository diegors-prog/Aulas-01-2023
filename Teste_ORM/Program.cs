// See https://aka.ms/new-console-template for more information
using System;
using System.Linq;
using Data;
using Domain;

using var db = new DataContext();

// Note: This sample requires the database to be created before running.
Console.WriteLine($"Database path: {db.DbPath}.");

// Create
Console.WriteLine("Inserting a new person");
var person = new Person { Id = 1, Name = "João da Silva" };
db.Add(person);
db.SaveChanges();


// Delete
// Console.WriteLine("Delete the blog");
// db.Remove(person);
// db.SaveChanges();
