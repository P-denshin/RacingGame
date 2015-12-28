using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingGame {
    class PlayerCar : Car {
        public PlayerCar() {
            HoraizontalPoint = RacingGame.HoraizontalPoint.Center;
        }

        /// <summary>
        /// 自分の車を移動させます。
        /// </summary>
        /// <param name="md">移動する方向</param>
        public void Move(MovingDirection md) {
            switch (md) {
                case MovingDirection.Left:
                    if (HoraizontalPoint == RacingGame.HoraizontalPoint.Right)
                        HoraizontalPoint = RacingGame.HoraizontalPoint.Center;
                     else 
                        HoraizontalPoint = RacingGame.HoraizontalPoint.Left;                    
                    break;
                case MovingDirection.Right:
                    if (HoraizontalPoint == RacingGame.HoraizontalPoint.Left)
                        HoraizontalPoint = RacingGame.HoraizontalPoint.Center;
                    else
                        HoraizontalPoint = RacingGame.HoraizontalPoint.Right;
                    break;
            }
        }
    }

    enum MovingDirection{
        Left,
        Right
    }
}
