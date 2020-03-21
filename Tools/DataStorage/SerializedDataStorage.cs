using System;
using System.Collections.Generic;
using System.IO;
using CSharpProgramming2020.Task2.Models;
using CSharpProgramming2020.Task2.Tools.Managers;

namespace CSharpProgramming2020.Task2.Tools.DataStorage
{
    internal class SerializedDataStorage : IDataStorage
    {
        private readonly List<Person> _users;

        internal SerializedDataStorage()
        {
            try
            {
                _users = SerializationManager.Deserialize<List<Person>>(FileFolderHelper.StorageFilePath);
            }
            catch (FileNotFoundException)
            {
                _users = new List<Person>();
                string[] names =
                {
                    "Johann",
                    "Wolfgang",
                    "Ludwig van",
                    "Richard",
                    "Joseph",
                    "Johannes",
                    "Franz",
                    "Robert",
                    "Georg",
                    "Piotr",
                    "Felix",
                    "Antonin",
                    "Franz",
                    "Frederic",
                    "Igor",
                    "Giuseppe",
                    "Gustav",
                    "Sergej",
                    "Dmitrii",
                    "Richard",
                    "Hector",
                    "Claude",
                    "Giacomo",
                    "Giovanni",
                    "Anton",
                    "Georg",
                    "Camille",
                    "Jean",
                    "Maurice",
                    "Gioacchino",
                    "Edvard",
                    "Christoph",
                    "Paul",
                    "Claudio",
                    "Bela",
                    "Cesar",
                    "Antonio",
                    "Georges",
                    "Modest",
                    "Jean",
                    "Gabriel",
                    "Nikolai",
                    "Gaetano",
                    "Ralph",
                    "Bedrich",
                    "Johann",
                    "Carl",
                    "Leos",
                    "Francois",
                    "Alexandr",
                };
                string[] surnames =
                {
                    "Bach",
                    "Mozart",
                    "Beethoven",
                    "Wagner",
                    "Haydn",
                    "Brahms",
                    "Schubert",
                    "Schumann",
                    "Handel",
                    "Tchaikovskiy",
                    "Mendelssohn",
                    "Dvorak",
                    "Liszt",
                    "Chopin",
                    "Stravinskiy",
                    "Verdi",
                    "Mahler",
                    "Prokofiew",
                    "Shostakovich",
                    "Strauss",
                    "Berlioz",
                    "Debussy",
                    "Puccini",
                    "Palestrina",
                    "Bruckner",
                    "Teleman",
                    "Saens",
                    "Sibelius",
                    "Ravel",
                    "Rossini",
                    "Grieg",
                    "Gluck",
                    "Hindemith",
                    "Monteverdi",
                    "Bartock",
                    "Franck",
                    "Vivaldi",
                    "Bizet",
                    "Mussorgsky",
                    "Rameu",
                    "Faure",
                    "Korsakov",
                    "Donizetti",
                    "Williams",
                    "Smetana",
                    "Strauss",
                    "Weber",
                    "Janacek",
                    "Couperin",
                    "Borodin"
                };
                string[] mails =
                {
                    "bachthebest",
                    "mozart",
                    "LVbeth",
                    "wagnerboi",
                    "yourhaydn",
                    "abrahms",
                    "shuberty",
                    "schuMann",
                    "gHandel",
                    "ptchaikovskiy",
                    "mndlsn",
                    "dvorak",
                    "liszt",
                    "chopin",
                    "strvky",
                    "verdi",
                    "mahler",
                    "prokofiew",
                    "shostakovich",
                    "strauss",
                    "berlioz",
                    "debussy",
                    "puccini",
                    "palestrina",
                    "bruckner",
                    "teleman",
                    "saens",
                    "sibelius",
                    "ravel",
                    "rossini",
                    "grieg",
                    "gluck",
                    "hindemith",
                    "monteverdi",
                    "bartock",
                    "franck",
                    "vivaldi",
                    "bizet",
                    "mussorgsky",
                    "rameu",
                    "faure",
                    "korsakov",
                    "donizetti",
                    "williams",
                    "smetana",
                    "strauss",
                    "weber",
                    "janacek",
                    "couperin",
                    "borodin"
                };
                Random random = new Random();
                
                for (int i = 0; i < 50; ++i)
                {
                    try
                    {
                        DateTime birthday = new DateTime(random.Next(1950, 2012), random.Next(1, 12),
                            random.Next(1, 30));
                        Person person = new Person(names[i], surnames[i], mails[i] + "@gmail.com", birthday);
                        _users.Add(person);
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        --i;
                    }
                }

                SaveChanges();
            }
        }

        public void Add(Person person)
        {
            _users.Add(person);
            SaveChanges();
        }

        public void Remove(Person person)
        {
            _users.Remove(person);
            SaveChanges();
        }

        public void DoChanges()
        {
            SaveChanges();
        }

        public List<Person> PersonsList
        {
            get { return _users; }
        }

        private void SaveChanges()
        {
            SerializationManager.Serialize(_users, FileFolderHelper.StorageFilePath);
        }
    }
}
