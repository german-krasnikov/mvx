using Game.Views.Planets;
using Modules.Planets;
using Zenject;

namespace Game.Presenters.Planets
{
    public class PlanetPresenter
    {
        public class Factory : PlaceholderFactory<Planet, PlanetView, PlanetPresenter>
        {
        }
        
        public PlanetPresenter(Planet planet, PlanetView view)
        {
            view.SetLock(false);
        }
    }
}