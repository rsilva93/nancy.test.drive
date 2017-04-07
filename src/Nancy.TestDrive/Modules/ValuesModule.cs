using Nancy.ModelBinding;
using Nancy.Responses.Negotiation;
using System.Collections.Generic;
using System.Linq;

namespace Nancy.TestDrive.Modules
{
    public class ValuesModule : NancyModule
    {
        private static readonly IList<Model> _models = new List<Model>();

        public ValuesModule()
        {
            Get("/api/values", x => All());
            Get("/api/values/{id}", x => One(x.id));
            Post("/api/values", x => Insert());
            Put("/api/values/{id}", x => Update(x.id));
            Delete("/api/values/{id}", x => Remove(x.id));
        }

        public IHideObjectMembers All()
        {
            return Negotiate.WithStatusCode(HttpStatusCode.OK).WithModel(_models);
        }

        public IHideObjectMembers One(int id)
        {
            var model = _models.FirstOrDefault(x => x.Id == id);

            if (model == null)
            {
                return Negotiate.WithStatusCode(HttpStatusCode.NotFound);
            }

            return Negotiate.WithStatusCode(HttpStatusCode.OK).WithModel(model);
        }

        public IHideObjectMembers Insert()
        {
            var newModel = this.Bind<Model>();

            if (string.IsNullOrWhiteSpace(newModel.Value))
            {
                return Negotiate.WithStatusCode(HttpStatusCode.BadRequest);
            }

            newModel.Id = _models.Count + 1;

            _models.Add(newModel);

            return Negotiate.WithStatusCode(HttpStatusCode.Created).WithModel(newModel);
        }

        public IHideObjectMembers Update(int id)
        {
            var model = _models.FirstOrDefault(x => x.Id == id);

            if (model == null)
            {
                return Negotiate.WithStatusCode(HttpStatusCode.NotFound);
            }

            var newModel = this.Bind<Model>();

            if (string.IsNullOrWhiteSpace(newModel.Value))
            {
                return Negotiate.WithStatusCode(HttpStatusCode.BadRequest);
            }

            model.Value = newModel.Value;

            return Negotiate.WithStatusCode(HttpStatusCode.OK);
        }

        public IHideObjectMembers Remove(int id)
        {
            var model = _models.FirstOrDefault(x => x.Id == id);

            if (model == null)
            {
                return Negotiate.WithStatusCode(HttpStatusCode.NotFound);
            }

            _models.Remove(model);

            return Negotiate.WithStatusCode(HttpStatusCode.OK);
        }

        public class Model
        {
            public int Id { get; set; }
            public string Value { get; set; }
        }
    }
}
