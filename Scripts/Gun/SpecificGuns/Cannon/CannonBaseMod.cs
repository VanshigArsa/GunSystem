﻿using System;
using UnityEngine;
using GunClasses.AmmoClasses;

namespace GunClasses.CannonClasses
{
    public class CannonBaseMod : GunMod, IAmmoRequiredGunMod
    {
        public event Action<int> AmmoRequest;

        public event Action OnCannonBaseModFire;

        private CannonInput _cannonInput;
        private D_CannonBaseMod D_cannonBaseMod;

        protected GunAmmoRequest _ammoRequest;

        public CannonBaseMod(CannonInput cannonInput, D_CannonBaseMod D_cannonBaseMod, GunBulletAmmoContainer bulletAmmoContainer)
        {
            _cannonInput = cannonInput;

            this.D_cannonBaseMod = D_cannonBaseMod;

            _bulletAmmoContainer = bulletAmmoContainer;

            cannonInput.OnBaseModFireInputDown += OnInputDown;

            OnCannonBaseModFire += SetLastTimeFired;
            OnCannonBaseModFire += RequestAmmo;
            OnCannonBaseModFire += SpawnProjectile;
        }

        private void OnInputDown()
        {
            if (_ammoRequest.GetAmmoValue() > 0 && GetTimeSinceLastFirePassed(D_cannonBaseMod.MinTimeBtwFire))
            {
                OnCannonBaseModFire?.Invoke();
            }
        }

        private void SpawnProjectile()
        {

        }

        public void RequestAmmo()
        {
            _bulletAmmoContainer.CallOnModifyAmmoEvent(-D_cannonBaseMod.AmmoCost);
        }
    }
}
