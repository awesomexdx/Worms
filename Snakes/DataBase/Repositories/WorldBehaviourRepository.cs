using System;
using System.Collections.Generic;
using Snakes.DataBase.Base;
using Snakes.DataBase.Models;

namespace Snakes.DataBase.Repositories
{
    class WorldBehaviourRepository : IWorldBehaviourRepository
    {
        private readonly MainDbContext _сontext;
        public WorldBehaviourRepository(MainDbContext сontext)
        {
            _сontext = сontext;
        }
        public bool Create(WorldBehaviourModel item, bool autoincrement = true)
        {
            try
            {
                if (autoincrement) item.Id = 0;
                _сontext.WorldBehaviours.Add(item);
                _сontext.SaveChanges();
                return true;
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
                return false;
            }
        }

        public WorldBehaviourModel Delete(int id)
        {
            WorldBehaviourModel wbm = Get(id);

            if (wbm != null)
            {
                _сontext.WorldBehaviours.Remove(wbm);
                _сontext.SaveChanges();
            }

            return wbm;
        }

        public IEnumerable<WorldBehaviourModel> Get()
        {
            return _сontext.WorldBehaviours;
        }

        public WorldBehaviourModel Get(int id)
        {
            return _сontext.WorldBehaviours.Find(id);
        }

        public void Update(WorldBehaviourModel model)
        {
            throw new NotImplementedException();
        }
    }
}
