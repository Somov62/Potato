using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Potato
{
    public class Pickle : INotifyPropertyChanged
    {
        private int _health;
        private string _name;
        private int _protection;
        private int _damage;
        public Pickle()
        {

        }
        public Pickle(string name, int helth, int protection, int damage)
        {
            Name = name;
            Health = helth;
            Protection = protection;
            Damage = damage;
        }
        public string Name 
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public int Health
        {
            get => _health;
            set
            {
                if (value < 1)
                {
                    this.Die();
                    _health = 0;
                    OnPropertyChanged(nameof(Health));
                    return;
                }
                _health = value;
                OnPropertyChanged(nameof(Health));
            }
        }
        public int Protection 
        {
            get => _protection;
            set
            {
                _protection = value;
                OnPropertyChanged(nameof(Protection));
            }
        }
        public int Damage
        {
            get => _damage;
            set
            {
                _damage = value;
                OnPropertyChanged(nameof(Damage));
            }
        }

        public delegate void DiedHandler(string name);
        public event DiedHandler OnDied;
        public event PropertyChangedEventHandler PropertyChanged;

        public (bool, int) Attack(Pickle targetPickle)
        {
            if (targetPickle.Protection >= this.Damage) return (false, 0);
            Random rnd = new Random();
            int damage = this.Damage - Convert.ToInt32(targetPickle.Protection * rnd.NextDouble());
            targetPickle.Health -= damage;
            return (true, damage);
        }

        public void Die()
        {
            OnDied?.Invoke(this.Name);
        }

        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }


}
