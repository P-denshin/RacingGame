using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingGame {
    class EnemyCar : Car {
        int verticalPoint;

        public int VerticalPoint {
            get { 
                return verticalPoint; 
            }
            set {
                if (value < 5 && value >= 0) { verticalPoint = value; } 
            }
        }

        public EnemyCar(HoraizontalPoint hp, int vp) {
            this.HoraizontalPoint = hp;
            this.verticalPoint = vp;
        }

        /// <summary>
        /// 更新する。
        /// </summary>
        /// <returns>trueは現存, falseは消える</returns>
        public bool Update() {
            if (verticalPoint >= 5) { return false; }
            verticalPoint++;
            return true;
        }
    }
}
