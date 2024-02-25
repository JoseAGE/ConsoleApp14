//Escribir un programa que lea una lista de estudiantes de un archivo y cree una lista enlazada.
//Cada entrada de la lista enlazada ha de tener el nombre del estudiante, un puntero al siguiente estudiante y un puntero a una lista enlazada de calificaciones.
//Por ejemplo, cinco calificaciones por cada estudiante.

using System;
using System.Collections.Generic;
using System.IO;

// Clase para representar una calificación
class Grade
{
    public int Score { get; set; }
    public Grade Next { get; set; }

    public Grade(int score)
    {
        Score = score;
        Next = null;
    }
}

// Clase para representar un estudiante
class Student
{
    public string Name { get; set; }
    public Student Next { get; set; }
    public Grade Grades { get; set; }

    public Student(string name)
    {
        Name = name;
        Next = null;
        Grades = null;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Ruta del archivo de entrada
        string filePath = @"C:\Users\Jose Guzman\OneDrive\Documentos\estudiantes.txt";

        // Crear la lista enlazada de estudiantes
        Student head = null;
        Student current = null;

        // Leer el archivo y crear la lista enlazada de estudiantes
        try
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] data = line.Split(',');
                    string studentName = data[0].Trim();
                    Student newStudent = new Student(studentName);

                    // Agregar calificaciones al estudiante
                    for (int i = 1; i < data.Length; i++)
                    {
                        int score = int.Parse(data[i].Trim());
                        Grade newGrade = new Grade(score);
                        if (newStudent.Grades == null)
                        {
                            newStudent.Grades = newGrade;
                        }
                        else
                        {
                            Grade temp = newStudent.Grades;
                            while (temp.Next != null)
                            {
                                temp = temp.Next;
                            }
                            temp.Next = newGrade;
                        }
                    }

                    // Agregar estudiante a la lista enlazada
                    if (head == null)
                    {
                        head = newStudent;
                        current = head;
                    }
                    else
                    {
                        current.Next = newStudent;
                        current = current.Next;
                    }
                }
            }

            // Imprimir la lista enlazada de estudiantes y sus calificaciones
            PrintStudentList(head);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error al leer el archivo: " + e.Message);
        }
    }

    // Método para imprimir la lista enlazada de estudiantes y sus calificaciones
    static void PrintStudentList(Student head)
    {
        Student current = head;
        while (current != null)
        {
            Console.WriteLine("Nombre del estudiante: " + current.Name);
            Console.Write("Calificaciones: ");
            Grade gradeCurrent = current.Grades;
            while (gradeCurrent != null)
            {
                Console.Write(gradeCurrent.Score + " ");
                gradeCurrent = gradeCurrent.Next;
            }
            Console.WriteLine();
            current = current.Next;
        }
    }
}