using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingGame {
    class Simulator {
        List<EnemyCar> enemysCar;
        PlayerCar playerCar;
        bool isNew;

        public Simulator() {
            enemysCar = new List<EnemyCar>();
            playerCar = new PlayerCar();
            isNew = false;
        }

        /// <summary>
        /// 敵を更新する。
        /// </summary>
        /// <returns>trueはゲーム継続, falseは終了</returns>
        public bool EnemiesUpdate() {
            List<EnemyCar> rml = new List<EnemyCar>();
            enemysCar.ForEach((a) => {
                if (!a.Update()) { rml.Add(a); }   //更新しつつ
            });
            rml.ForEach(a => enemysCar.Remove(a));

            //作る
            if (isNew) {
                Random rm = new Random();
                int n = rm.Next() % 2 + 1;
                for (int i = 0; i < n; i++) {
                    HoraizontalPoint hp;

                    int k = rm.Next() % 3;
                    if (k == 0) hp = HoraizontalPoint.Left;
                    else if (k == 1) hp = HoraizontalPoint.Center;
                    else hp = HoraizontalPoint.Right;

                    enemysCar.Add(new EnemyCar(hp, 0));
                }
            }
            isNew = !isNew;


            return detection();
        }

        /// <summary>
        ///  自機を更新する。
        /// </summary>
        /// <param name="md">移動方向</param>
        public bool PlayerUpdate(MovingDirection md) {
            playerCar.Move(md);
            return detection();
        }

        private bool detection() {
            var under = enemysCar.FindAll((a) => { if (a.VerticalPoint <= 4) return true; else return false; });

            foreach (var i in under) {
                if (i.HoraizontalPoint == playerCar.HoraizontalPoint && i.VerticalPoint == 4) {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 自機の場所を取得する。
        /// </summary>
        public HoraizontalPoint PlayerCarPoint {
            get {
                return playerCar.HoraizontalPoint;
            }
        }

        /// <summary>
        /// 敵機の場所を取得する。
        /// </summary>
        public List<EnemyPoint> EnemiesPoint {
            get {
                List<EnemyPoint> l = new List<EnemyPoint>();
                foreach (var i in enemysCar) {
                    l.Add(new EnemyPoint() { hp = i.HoraizontalPoint, vp = i.VerticalPoint });
                }
                return l;
            }
        }
    }

    class EnemyPoint {
        public HoraizontalPoint hp;
        public int vp;
    }
}
