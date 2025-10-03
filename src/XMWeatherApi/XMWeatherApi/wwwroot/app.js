/* const for app.js */
const apiBase = "/api";
/* element bofy render */
const countriesEl = document.getElementById('countries');
const citiesEl = document.getElementById('cities');
const out = document.getElementById('output');
const btn = document.getElementById('fetch');

/* init for startup index.html */
async function init() {
    const res = await fetch(`${apiBase}/countries`);
    const countries = await res.json();
    countriesEl.innerHTML = '';
    countries.forEach(c => {
        const opt = document.createElement('option');
        opt.value = c.code;
        opt.textContent = c.name;
        countriesEl.appendChild(opt);
    });
    countriesEl.addEventListener('change', onCountryChange);
    citiesEl.addEventListener('change', onCityChange);
}

async function onCountryChange() {
    const code = countriesEl.value;
    citiesEl.innerHTML = '<option>Loading...</option>';
    const res = await fetch(`${apiBase}/countries/${code}/cities`);
    if (!res.ok) { citiesEl.innerHTML = '<option>Error loading</option>'; return; }
    const cities = await res.json();
    citiesEl.innerHTML = '';
    cities.forEach(c => {
        const opt = document.createElement('option'); opt.value
            = c.name; opt.textContent = c.name; citiesEl.appendChild(opt);
    });
}
function onCityChange() { /* render code here if needed */ }
/* click event listener */
btn.addEventListener('click', async () => {
    const city = citiesEl.value;
    const country = countriesEl.value;
    if (!city) { out.textContent = 'Select a city'; return; }
    const res = await fetch(`${apiBase}/weather/${encodeURIComponent(city)}?
countryCode=${encodeURIComponent(country)}`);
    if (!res.ok) { out.textContent = `Error ${res.status}`; return; }
    const w = await res.json();
    out.textContent = JSON.stringify(w, null, 2);
});

init();
