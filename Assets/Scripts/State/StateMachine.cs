using Interface;

namespace State
{
    public class StateMachine<T>
    {
        private readonly T _context;
        private IState<T> _currentState;

        public StateMachine(T context)
        {
            _context = context;
        }

        public void ChangeState(IState<T> newState)
        {
            _currentState?.OnExit(_context);
            _currentState = newState;
            _currentState?.OnEnter(_context);
        }

        public void Update()
        {
            _currentState?.OnUpdate(_context);
        }
    }
}