using Soleil.Battle;
using Soleil.Misc;
using Soleil.Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soleil
{
    class PersonParty: INotifier
    {
        Person[] allCharacters;
        IListener[] listeners;

        /// <summary>
        /// CreateNewParty
        /// </summary>
        public PersonParty()
        {
            allCharacters = CreateNewParty();
            listeners = new IListener[(int)ListenerType.size];
            Person[] CreateNewParty()
            {
                Person[] result = new Person[(int)CharaName.size];
                result[(int)CharaName.Lune] = new Person(CharaName.Lune);
                result[(int)CharaName.Sunny] = new Person(CharaName.Sunny);
                result[(int)CharaName.Tella] = new Person(CharaName.Tella);
                return result;
            }
        }

        /// <summary>
        /// CreateFromSaveData
        /// </summary>
        public PersonParty(Person[] people)
        {
            allCharacters = people;
        }

        public Person Get(CharaName name)
        {
            return allCharacters[(int)name];
        }

        public int GetPartyNum() => GetActiveMembers().Length;

        public void SetActive(CharaName name, bool active)
        {
            allCharacters[(int)name].InParty = active;
            OnRefresh();
        }

        public Person[] GetActiveMembers()
        {
            var result = new List<Person>();
            for (int i = 0; i < allCharacters.Length; i++)
            {
                var target = allCharacters[i];
                if (!target.InParty) continue;
                result.Add(target);
            }
            return result.ToArray();
        }

        public bool GetActive(CharaName name)
        {
            return allCharacters[(int)name].InParty;
        }

        public void AddListener(IListener listener)
            => listeners[(int)listener.Type] = listener;

        private void OnRefresh()
        {
            listeners.ForEach2(l => l?.OnListen(this));
        }
    }
}
