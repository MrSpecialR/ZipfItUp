﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using ZipfItUp.Data;
using ZipfItUp.Models;
using ZipfItUp.TextManipulator;

namespace ZipfItUp.ModelHelper
{
    public static class DatabaseInsert
    {
        public static void Words(List<Word> words, OccuranceContext context)
        {
            List<Word> AllWords = context.Words.ToList();
            List<Word> WordsToInsert = new List<Word>();
            foreach (Word word in words)
            {
                Word exists = AllWords.FirstOrDefault(x => x.WordString == word.WordString);
                if (exists != null)
                {
                    exists.Occurances += word.Occurances;
                }
                else
                {
                    WordsToInsert.Add(word);
                }
            }
            context.Words.AddRange(WordsToInsert);
            context.SaveChanges();
        }
        public static void DocumentWords(List<Word> words, OccuranceContext context, string path)
        {
            Document document = Document(path);
            DatabaseInsert.Words(words,context);

            List<Word> AllWords = context.Words.ToList();
            List<DocumentWord> WordsToAdd = new List<DocumentWord>();
            foreach (Word word in words)
            {
                WordsToAdd.Add(new DocumentWord()
                {
                    Occurances = word.Occurances,
                    Document = document,
                    Word = AllWords.First(x=>x.WordString==word.WordString)
                });
            }
            document.Words = WordsToAdd;
            document.WordCount = WordsToAdd.Sum(x => x.Occurances);
            context.Documents.Add(document);
            context.SaveChanges();
        }

        public static Document Document(string path)
        {
            return new Document()
            {
                   DocumentType = Transform.GetType(path),
                   FilePath = path,
                   FileName = TextManipulator.TextExtractor.GetFileName(path),
                   DateUploaded = DateTime.Now
            };
        }

        public static void UploadFileToDatabase(string path)
        {
            var wordlist = GetWords.WordList(path);
            List<Word> words = ModelHelper.Transform.ToWords(wordlist.ToList());
            using (OccuranceContext context = new OccuranceContext())
            {
                DocumentWords(words, context, path);
            }
        }
    }
}