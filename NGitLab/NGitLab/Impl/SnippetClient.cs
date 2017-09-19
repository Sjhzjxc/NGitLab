﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NGitLab.Models;

namespace NGitLab.Impl
{
    public class SnippetClient : ISnippetClient
    {
        private const string ProjectUrl = "/projects";
        private const string SnippetUrl = "/snippets";
        private const string ProjectSnippetsUrl = ProjectUrl + "/{0}/snippets";
        private const string SingleSnippetUrl = ProjectUrl + "/{0}/snippets/{1}";

        private readonly API _api;

        public SnippetClient(API api)
        {
            _api = api;
        }

        public IEnumerable<Snippet> All => _api.Get().GetAll<Snippet>(SnippetUrl); // all snippet of the user

        public IEnumerable<Snippet> ForProject(int projectId)
        {
            return _api.Get().GetAll<Snippet>(string.Format(ProjectSnippetsUrl, projectId));
        }

        public Snippet Get(int projectId, int snippetId)
        {
            return _api.Get().To<Snippet>(string.Format(SingleSnippetUrl, projectId, snippetId));
        }

        public void Create(SnippetCreate snippet)
        {
            _api.Post().With(snippet).To<SnippetCreate>(SnippetUrl);
        }

        public void Create(SnippetProjectCreate snippet)
        {
            _api.Post().With(snippet).To<SnippetProjectCreate>(string.Format(ProjectSnippetsUrl, snippet.Id));
        }

        public void Delete(int snippetId) => _api.Delete().Execute(string.Concat(SnippetUrl, "/", snippetId));

        public void Delete(int projectId, int snippetId) => _api.Delete().Execute(string.Format(SingleSnippetUrl, projectId, snippetId));
    }
}
