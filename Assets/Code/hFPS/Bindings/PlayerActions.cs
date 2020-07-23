using InControl;

namespace hFPS.Bindings
{
    public class PlayerActions : PlayerActionSet
    {
        private readonly PlayerAction _moveLeft;
        private readonly PlayerAction _moveRight;
        private readonly PlayerAction _moveForward;
        private readonly PlayerAction _moveBack;
        public readonly PlayerTwoAxisAction Move;

        private readonly PlayerAction _lookLeft;
        private readonly PlayerAction _lookRight;
        private readonly PlayerAction _lookUp;
        private readonly PlayerAction _lookDown;
        public readonly PlayerTwoAxisAction Look;

        public readonly PlayerAction Jump;

        private PlayerActions()
        {
            _moveLeft = CreatePlayerAction("Move Left");
            _moveRight = CreatePlayerAction("Move Right");
            _moveForward = CreatePlayerAction("Move Forward");
            _moveBack = CreatePlayerAction("Move Back");
            Move = CreateTwoAxisPlayerAction(_moveLeft, _moveRight, _moveBack, _moveForward);

            _lookLeft = CreatePlayerAction("Look Left");
            _lookRight = CreatePlayerAction("Look Right");
            _lookUp = CreatePlayerAction("Look Up");
            _lookDown = CreatePlayerAction("Look Down");
            Look = CreateTwoAxisPlayerAction(_lookLeft, _lookRight, _lookDown, _lookUp);

            Jump = CreatePlayerAction("Jump");
        }

        public static PlayerActions CreateWithDefaultBindings()
        {
            var playerActions = new PlayerActions();

            // LOOK
            playerActions._lookLeft.AddDefaultBinding(Mouse.NegativeX);
            playerActions._lookLeft.AddDefaultBinding(InputControlType.RightStickLeft);
            
            playerActions._lookRight.AddDefaultBinding(Mouse.PositiveX);
            playerActions._lookRight.AddDefaultBinding(InputControlType.RightStickRight);
            
            playerActions._lookDown.AddDefaultBinding(Mouse.NegativeY);
            playerActions._lookDown.AddDefaultBinding(InputControlType.RightStickDown);
            
            playerActions._lookUp.AddDefaultBinding(Mouse.PositiveY);
            playerActions._lookUp.AddDefaultBinding(InputControlType.RightStickUp);
            
            // MOVE
            playerActions._moveLeft.AddDefaultBinding(Key.A);
            playerActions._moveLeft.AddDefaultBinding(InputControlType.LeftStickLeft);
            
            playerActions._moveRight.AddDefaultBinding(Key.D);
            playerActions._moveRight.AddDefaultBinding(InputControlType.LeftStickRight);
            
            playerActions._moveBack.AddDefaultBinding(Key.S);
            playerActions._moveBack.AddDefaultBinding(InputControlType.LeftStickDown);
            
            playerActions._moveForward.AddDefaultBinding(Key.W);
            playerActions._moveForward.AddDefaultBinding(InputControlType.LeftStickUp);

            // Other
            playerActions.Jump.AddDefaultBinding(Key.Space);
            playerActions.Jump.AddDefaultBinding(InputControlType.Action3);
            
            return playerActions;
        }
    }
}
