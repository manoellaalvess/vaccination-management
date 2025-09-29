// Função genérica para chamar a API
async function callApi(url, method = "GET", body = null) {
  try {
    const options = {
      method,
      headers: { "Content-Type": "application/json" },
    };
    if (body) options.body = JSON.stringify(body);

    const response = await fetch(url, options);
    const data = await response.json();

    showOutput({ status: response.status, data });
  } catch (error) {
    showOutput({ status: "error", message: error.message });
  }
}

// Função para exibir resultados
function showOutput(result) {
  const output = document.getElementById("output");
  if (!output) return;
  output.innerHTML = `<pre>${JSON.stringify(result, null, 2)}</pre>`;
}

// ==================== EVENTOS ====================

// Pessoa
document.getElementById("p_create_person")?.addEventListener("click", async () => {
  const cpf = prompt("Digite o CPF da pessoa:");
  const name = prompt("Digite o nome da pessoa:");
  if (!cpf || !name) return alert("Preencha todos os campos!");

  await callApi("/CreatePerson", "POST", { cpf, name });
});

document.getElementById("p_delete_person")?.addEventListener("click", async () => {
  const cpf = prompt("Digite o CPF da pessoa:");
  if (!cpf) return;
  await callApi(`/api/Person/${cpf}`, "DELETE");
});

document.getElementById("p_list_people")?.addEventListener("click", async () => {
  await callApi("api/Person/GetAllPeople", "GET");
});

// Vacinas
document.getElementById("p_create_vaccine")?.addEventListener("click", async () => {
  const vaccineName = prompt("Digite o nome da vacina:");
  if (!vaccineName) return;
  await callApi("/CreateVaccine", "POST", { vaccineName });
});

document.getElementById("p_list_vaccine")?.addEventListener("click", async () => {
  await callApi("/GetAllVaccine", "GET");
});

// Vacinação
document.getElementById("p_register_vaccination")?.addEventListener("click", async () => {
  const personCpf = prompt("Digite o CPF da pessoa:");
  const vaccineId = parseInt(prompt("Digite o ID da vacina:"), 10);
  const dose = parseInt(prompt("Digite o número da dose:"), 10);

  if (!personCpf || !vaccineId || !dose) return alert("Preencha todos os campos!");

  await callApi("/AddVaccination", "POST", { personCpf, vaccineId, dose });
});

document.getElementById("p_delete_vaccination")?.addEventListener("click", async () => {
  const id = parseInt(prompt("Digite o ID da vacinação:"), 10);
  if (!id) return;
  await callApi(`/api/Vaccination/${id}`, "DELETE");
});

// Cartão de vacinação
document.getElementById("p_load_card")?.addEventListener("click", async () => {
  const cpf = prompt("Digite o CPF da pessoa:");
  if (!cpf) return;
  await callApi(`/api/Person/${cpf}/vaccination-card`, "GET");
});
