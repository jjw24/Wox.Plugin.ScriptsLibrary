﻿using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Wox.Infrastructure;

namespace Wox.Plugin.ScriptsLibrary.Commands
{
    public static class Library
    {
        public static List<Result> GetAvailableCommands()
        {
            var results = new List<Result>();
            results.AddRange(new[]
            {
                            new Result
                            {
                                Title = "Go to library",
                                SubTitle = "Got to library and see more details",
                                IcoPath = Main.IcoImagePath,
                                Score = 5,
                                Action = (e) =>
                                {
                                    Main._mainWindow.Visibility = Visibility.Visible;
                                    return true;
                                }
                            }
                            //new Result
                            //{
                            //    Title = "Show all scripts",
                            //    SubTitle = "See all the scripts that you can run",
                            //    IcoPath = Main.IcoImagePath,
                            //    Score = 5,
                            //    Action = (e) =>
                            //    {
                            //        Main._settings.ScriptList.Select(x => x)
                            //        .ToList();
                            //        return true;
                            //    }
                            //},
            });           

            return results;
        }

        public static List<Result> GetMatchingScripts(string querySearchString)
        {
            var matchedScripts = Main._settings.ScriptList
                                    .Where(x => StringMatcher.FuzzySearch(querySearchString, x.FileName)
                                                             .IsSearchPrecisionScoreMet())
                                    .Select(x => x)
                                    .ToList();

            if (matchedScripts.Count == 0)
                return new List<Result>();

            return matchedScripts.Select(c => new Result()
            {
                Title = c.FileName,
                SubTitle = c.Path,
                IcoPath = Main.IcoRunImagePath,
                Score = 5,
                Action = (e) =>
                {
                    CMDScript.RunCMDFromFileLink(c.Path);
                    return true;
                }
            }).ToList();
        }
    }
}