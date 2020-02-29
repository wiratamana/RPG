using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class Status_ST
    {
        private Status_Information information;
        public int MaxStamina
        {
            get
            {
                return information.HP;
            }
        }

        public int CurrentStamina { get; private set; }

        public EventManager<int> OnStaminaReducedBecauseAttackingEvent { private set; get; }
        public EventManager<int> OnStaminaReducedBecauseParryingEvent { private set; get; }
        public EventManager OnStaminaReducedToZeroEvent { private set; get; }
        public EventManager OnStaminaBeginRegeneratingEvent { private set; get; }
        public EventManager<int> OnStaminaRegeneratingEvent { private set; get; }
        public EventManager OnStaminaFullyRegeneratedEvent { private set; get; }
        public EventManager<int> OnStaminaHealedEvent { private set; get; }

        public bool IsStaminaFull => CurrentStamina == MaxStamina;
        public float StaminaFillRate => CurrentStamina / (float)MaxStamina;

        private Coroutine RegenerateStaminaCoroutine;
        private Coroutine WaitingBeforeStaminaStartToRegenerateCoroutine;
        private readonly float waitingTimeBeforeStaminaStartToRegenerateInSeconds = 2.0f;
        private WaitForSeconds waitTimeInSeconds;

        public Status_ST(Status_Information information)
        {
            this.information = information;

            OnStaminaReducedBecauseAttackingEvent = new EventManager<int>();
            OnStaminaReducedBecauseParryingEvent = new EventManager<int>();
            OnStaminaReducedToZeroEvent = new EventManager();
            OnStaminaBeginRegeneratingEvent = new EventManager();
            OnStaminaRegeneratingEvent = new EventManager<int>();
            OnStaminaFullyRegeneratedEvent = new EventManager();
            OnStaminaHealedEvent = new EventManager<int>();

            waitTimeInSeconds = new WaitForSeconds(waitingTimeBeforeStaminaStartToRegenerateInSeconds);

            CurrentStamina = MaxStamina;
        }

        public void Attack(int staminaUsage)
        {
            CurrentStamina = Mathf.Max(CurrentStamina - staminaUsage, 0);
            OnStaminaReducedBecauseAttackingEvent.Invoke(staminaUsage);

            if (CurrentStamina == 0)
            {
                OnStaminaReducedToZeroEvent.Invoke();
            }

            StartWaitingForStaminaToRegenerateCoroutine();
        }

        public void Parry(int staminaUsage)
        {
            CurrentStamina = Mathf.Max(CurrentStamina - staminaUsage, 0);
            OnStaminaReducedBecauseParryingEvent.Invoke(staminaUsage);

            if (CurrentStamina == 0)
            {
                OnStaminaReducedToZeroEvent.Invoke();
            }

            StartWaitingForStaminaToRegenerateCoroutine();
        }

        public void BeginRegenerate()
        {
            OnStaminaBeginRegeneratingEvent.Invoke();
        }

        public void Regenerate(int regeneratiOnRate)
        {
            var lastStamina = CurrentStamina;
            CurrentStamina = Mathf.Min(CurrentStamina + regeneratiOnRate, MaxStamina);

            OnStaminaRegeneratingEvent.Invoke(CurrentStamina - lastStamina);

            if (CurrentStamina == MaxStamina)
            {
                OnStaminaFullyRegeneratedEvent.Invoke();
            }
        }

        public void HealStamina(int healAmount)
        {
            CurrentStamina = Mathf.Min(CurrentStamina + healAmount, MaxStamina);
            OnStaminaHealedEvent.Invoke(healAmount);

            if (CurrentStamina == MaxStamina)
            {
                OnStaminaFullyRegeneratedEvent.Invoke();
            }
        }

        private void StartWaitingForStaminaToRegenerateCoroutine()
        {
            if(WaitingBeforeStaminaStartToRegenerateCoroutine != null)
            {
                GameManager.PlayerStatus.StopCoroutine(WaitingBeforeStaminaStartToRegenerateCoroutine);
                WaitingBeforeStaminaStartToRegenerateCoroutine = null;
            }

            if(RegenerateStaminaCoroutine != null)
            {
                GameManager.PlayerStatus.StopCoroutine(RegenerateStaminaCoroutine);
                RegenerateStaminaCoroutine = null;
            }

            WaitingBeforeStaminaStartToRegenerateCoroutine =
                GameManager.PlayerStatus.StartCoroutine(WaitingForStaminaToRegenerate());
        }

        private IEnumerator WaitingForStaminaToRegenerate()
        {
            yield return waitTimeInSeconds;

            RegenerateStaminaCoroutine =
                GameManager.PlayerStatus.StartCoroutine(RegenerateStamina());
        }

        private IEnumerator RegenerateStamina()
        {
            BeginRegenerate();

            while (IsStaminaFull == false)
            {
                Regenerate(Mathf.CeilToInt(100 * Time.deltaTime));
                yield return null;
            }
        }
    }
}
