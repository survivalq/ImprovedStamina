<h1 align="center">Improved Stamina</h1>

<p align="center">
  <img src="https://github.com/survivalq/ImprovedStamina/blob/main/Assets/icon.png" width="128" height="128">
</p>  

<p align="center">
  <img src="https://img.shields.io/badge/BepInEx-Required-blue" alt="BepInEx Required">
  <img src="https://img.shields.io/badge/Plugin-Version_1.2.0-brightgreen" alt="Plugin Version">
  <img src="https://img.shields.io/thunderstore/dt/Flopper/ImprovedStamina" alt="Downloads">
</p>

This is a mod that enhances the stamina regeneration system in the game. Instead of the default recharge behavior, this mod introduces a **dynamic stamina regen mechanic**:

#### Default Configuration:
- **Gradual Stamina Regeneration:** After stopping sprinting, stamina starts regenerating after **0.5 seconds**.
- **Scaling Regen Rate:** Over **3.0 seconds**, the regeneration rate increases up to **+8 stamina per second**.
- **More Natural Recovery:** The system ensures a smoother and more responsive stamina recovery experience.

This mod does not alter any other movement mechanics.

---

### Configuration

- You can update the configuration which is located at `BepInEx/config/ImprovedStamina.cfg`

<table>
  <thead>
    <tr>
      <th>Setting</th>
      <th>Default Value</th>
      <th>Max Value</th>
      <th>Description</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td><code>MaxRegenRate</code></td>
      <td>8.0</td>
      <td>25.0</td>
      <td>The maximum stamina regeneration multiplier.</td>
    </tr>
    <tr>
      <td><code>RegenRampUpTime</code></td>
      <td>3.0</td>
      <td>25.0</td>
      <td>The time (in seconds) it takes for stamina regeneration to reach max speed.</td>
    </tr>
    <tr>
      <td><code>DelayBeforeRegen</code></td>
      <td>0.5</td>
      <td>25.0</td>
      <td>The delay (in seconds) after stopping sprinting before regeneration starts.</td>
    </tr>
    <tr>
      <td><code>SprintDrainMultiplier</code></td>
      <td>1.0</td>
      <td>10.0</td>
      <td>Multiplier for stamina cost when sprinting. 1.0x is the game's default.</td>
    </tr>
  </tbody>
</table>

---

### Icon Credit
Huge thanks to **[StandingBlock](https://github.com/StandingBlock)** for creating the icon!