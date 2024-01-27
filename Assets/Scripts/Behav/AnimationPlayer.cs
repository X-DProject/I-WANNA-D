using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Game.Behav
{
    public enum   AnimParamType
    {
        None = 0, Int, Float, Bool, Trigger
    }
    public struct AnimParamData
    {
        public string Name { get; set; }      
        public string Value { get; set; }
        public AnimParamType Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static AnimParamData Parse(string[] args)
        {
            if (args == null)
                throw new ArgumentNullException(nameof(args), message:"[AnimArgumentData] cannot parse null args");

            return new AnimParamData()
            {
                Name = args[0],
                Type = args[1] switch
                {
                    string t when t.ToLower() == "int" => AnimParamType.Int,
                    string t when t.ToLower() == "bool" => AnimParamType.Bool,
                    string t when t.ToLower() == "float" => AnimParamType.Float,
                    string t when t.ToLower() == "trigger" => AnimParamType.Trigger,
                    _ => throw new ArgumentException($"[AnimArgumentData] unknown type: {args[1]}")
                },
                Value = args.Length > 2 ? args[2] : null
            };
        }
    }

    public sealed class AnimationPlayer : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;


        public void PlayDirectly(string pieceName) // call on UnityEvent
        {
            if (_animator == null)
                throw new MissingComponentException($"[AnimationPlayer] {name} missing animator.");

            _animator.Play(pieceName);
        }

        /*
         *  arg:
         *  name type [value]
         *  
         *  examples:
         *  speed float 10 => turn speed to 10
         *  die trigger    => turn trigger "die"
         */
        public void ChangeAnimParamDirectly(string arg) // call on UnityEvent
        {
            if (_animator == null)
                throw new MissingComponentException($"[AnimationPlayer] {name} missing animator.");

            if (string.IsNullOrEmpty(arg))
                throw new ArgumentNullException(nameof(arg), message: $"[AnimationPlayer] {name} has a empty arg.");

            var animParam = AnimParamData.Parse(arg.Split(' '));

            switch (animParam.Type)
            {
                case AnimParamType.Int:
                    _animator.SetInteger(animParam.Name, int.Parse(animParam.Value));
                    break;

                case AnimParamType.Bool:
                    _animator.SetBool(animParam.Name, bool.Parse(animParam.Value));
                    break;

                case AnimParamType.Float:
                    _animator.SetFloat(animParam.Name, float.Parse(animParam.Value));
                    break;

                case AnimParamType.Trigger:
                    _animator.SetTrigger(animParam.Name);
                    break;

                default:
                    throw new InvalidOperationException("[AnimationPlayer] cannot identify anim param type.");
            }
        }

        /*
         *  arg:
         *  name type endValue duration (type must be float)
         *  
         *  examples:
         *  speed float 10 5 => in 5 second, turn speed to 10 
         */
        public void ChangeAnimParamLerply(string arg) // call on UnityEvent
        {
            if (_animator == null)
                throw new MissingComponentException($"[AnimationPlayer] {name} missing animator.");

            if (string.IsNullOrEmpty(arg))
                throw new ArgumentNullException(nameof(arg), message: $"[AnimationPlayer] {name} has a empty arg.");

            var args = arg.Split(" ");

            if (args.Length != 4)
                throw new ArgumentException($"[AnimationPlayer] in {name}, arg must have 4 member when using lerp.");

            var animParam = AnimParamData.Parse(args);
            var duration  = float.Parse(args[3]);

            if (animParam.Type != AnimParamType.Float)
                throw new ArgumentException($"[AnimationPlayer] in {name}, arg type must be float when using lerp.");

            DOTween.To(
                getter: () => _animator.GetFloat(animParam.Name),
                setter: f  => _animator.SetFloat(name: animParam.Name, value: f),
                endValue: float.Parse(animParam.Value),
                duration: duration);
        }
    }
}
