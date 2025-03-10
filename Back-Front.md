# Calculadora de IMC e Calorias

## HTML

```html
<!DOCTYPE html>
<html lang="pt-BR">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Calculadora de IMC e Calorias</title>
    <link rel="stylesheet" href="style.css">
</head>
<body>
    <header>
        <h1>ContaCalorias - Senac - Grupo 19</h1>
    </header>

    <div id="imc-calculation" class="interaction-box">
        <h2>Calculadora de IMC</h2>
        <form id="imc-form">
            <label for="weight">Peso (kg):</label>
            <input type="number" id="weight" step="0.1" required>

            <label for="height">Altura (m):</label>
            <input type="number" id="height" step="0.01" required>

            <button type="submit">Calcular IMC</button>
        </form>

        <div id="imc-result">
            <h3>Seu IMC é: <span id="result"></span></h3>
            <p id="category"></p>
        </div>
    </div>

    <div id="calories-calculation" class="interaction-box">
        <h2>Calculadora de Calorias</h2>
        <form id="calories-form">
            <label for="food">Alimento:</label>
            <select id="food" required></select>

            <label for="food-quantity">Quantidade (gramas):</label>
            <input type="number" id="food-quantity" step="1" min="1" required>

            <button type="button" id="add-food">Adicionar Alimento</button>

            <div id="food-list"></div>

            <label for="exercise">Exercício:</label>
            <select id="exercise" required></select>

            <label for="exercise-time">Tempo (minutos):</label>
            <input type="number" id="exercise-time" step="1" min="1" required>

            <button type="button" id="add-exercise">Adicionar Exercício</button>

            <div id="exercise-list"></div>
        </form>

        <div id="calories-summary">
            <h2>Resumo Diário</h2>
            <p>Calorias Ingeridas: <span id="calories-ingested-today">0</span></p>
            <p>Calorias Eliminadas: <span id="calories-burned-today">0</span></p>
            <p>Saldo de Calorias: <span id="calories-balance-today">0</span></p>
        </div>
    </div>

    <footer>
        <p>&copy; 2025 ContaCalorias. Todos os direitos reservados.</p>
    </footer>

    <script src="script.js"></script>
</body>
</html>
```

## JavaScript

```javascript
document.addEventListener('DOMContentLoaded', function() {
    let totalCaloriesIngested = 0;
    let totalCaloriesBurned = 0;

    const alimentos = [
        { nome: 'Arroz branco', caloriasPor100g: 130 },
        { nome: 'Feijão preto', caloriasPor100g: 91 },
        { nome: 'Banana', caloriasPor100g: 89 },
        { nome: 'Maçã', caloriasPor100g: 52 },
        { nome: 'Pão integral', caloriasPor100g: 252 },
        { nome: 'Batata doce', caloriasPor100g: 86 },
        { nome: 'Frango grelhado', caloriasPor100g: 165 },
        { nome: 'Carne bovina', caloriasPor100g: 250 },
        { nome: 'Peixe (salmão)', caloriasPor100g: 206 },
        { nome: 'Ovo cozido', caloriasPor100g: 155 },
        { nome: 'Leite integral', caloriasPor100g: 61 },
        { nome: 'Queijo mussarela', caloriasPor100g: 280 },
        { nome: 'Iogurte natural', caloriasPor100g: 61 },
        { nome: 'Brócolis', caloriasPor100g: 34 },
        { nome: 'Cenoura', caloriasPor100g: 41 },
        { nome: 'Tomate', caloriasPor100g: 18 },
        { nome: 'Alface', caloriasPor100g: 15 },
        { nome: 'Espinafre', caloriasPor100g: 23 },
        { nome: 'Pão francês', caloriasPor100g: 268 },
        { nome: 'Laranja', caloriasPor100g: 47 },
        { nome: 'Manga', caloriasPor100g: 60 },
        { nome: 'Melancia', caloriasPor100g: 30 },
        { nome: 'Abacate', caloriasPor100g: 160 },
        { nome: 'Morango', caloriasPor100g: 32 },
        { nome: 'Amêndoas', caloriasPor100g: 576 },
        { nome: 'Nozes', caloriasPor100g: 654 },
        { nome: 'Avelãs', caloriasPor100g: 628 },
        { nome: 'Castanha de caju', caloriasPor100g: 553 },
        { nome: 'Pistache', caloriasPor100g: 562 },
        { nome: 'Azeitona preta', caloriasPor100g: 115 },
        { nome: 'Coco', caloriasPor100g: 354 },
        { nome: 'Lentilha', caloriasPor100g: 116 },
        { nome: 'Grão-de-bico', caloriasPor100g: 164 },
        { nome: 'Quinoa', caloriasPor100g: 120 },
        { nome: 'Aveia', caloriasPor100g: 389 },
        { nome: 'Granola', caloriasPor100g: 471 },
        { nome: 'Mel', caloriasPor100g: 304 },
        { nome: 'Cereal matinal', caloriasPor100g: 379 },
        { nome: 'Biscoito de água e sal', caloriasPor100g: 430 },
        { nome: 'Macarrão cozido', caloriasPor100g: 131 },
        { nome: 'Pizza de mussarela', caloriasPor100g: 266 },
        { nome: 'Coxinha de frango', caloriasPor100g: 282 },
        { nome: 'Pastel de queijo', caloriasPor100g: 333 },
        { nome: 'Pão de queijo', caloriasPor100g: 290 },
        { nome: 'Torta de frango', caloriasPor100g: 251 },
        { nome: 'Empada de camarão', caloriasPor100g: 270 }
    ];

    const exercicios = [
        { nome: 'Caminhada leve', caloriasPorMinuto: 5 },
        { nome: 'Corrida', caloriasPorMinuto: 10 },
        { nome: 'Natação', caloriasPorMinuto: 8.33 },
        { nome: 'Ciclismo', caloriasPorMinuto: 6.67 },
        { nome: 'Musculação', caloriasPorMinuto: 8.33 },
        { nome: 'Ioga', caloriasPorMinuto: 6.67 },
        { nome: 'Dança', caloriasPorMinuto: 6.67 },
        { nome: 'Pular corda', caloriasPorMinuto: 10 },
        { nome: 'Aula de spinning', caloriasPorMinuto: 11.67 },
        { nome: 'Pilates', caloriasPorMinuto: 6.67 },
        { nome: 'Boxe', caloriasPorMinuto: 11.67 },
        { nome: 'Treinamento HIIT', caloriasPorMinuto: 13.33 },
        { nome: 'Caminhada moderada', caloriasPorMinuto: 6.67 },
        { nome: 'Subida de escadas', caloriasPorMinuto: 8.33 },
        { nome: 'Escalada', caloriasPorMinuto: 10 },
        { nome: 'Artes marciais', caloriasPorMinuto: 11.67 },
        { nome: 'Hidroginástica', caloriasPorMinuto: 8.33 },
        { nome: 'Jiu-jitsu', caloriasPorMinuto: 10 },
        { nome: 'Futebol', caloriasPorMinuto: 10 },
        { nome: 'Basquete', caloriasPorMinuto: 10 },
        { nome: 'Tênis', caloriasPorMinuto: 10 },
        { nome: 'Vôlei', caloriasPorMinuto: 8.33 }
    ];

    function updateFoodOptions() {
        const foodSelect = document.getElementById('food');
        foodSelect.innerHTML = '<option value="">Selecione um alimento</option>';
        alimentos.forEach(alimento => {
            const option = document.createElement('option');
            option.value = alimento.nome;
            option.textContent = alimento.nome;
            foodSelect.appendChild(option);
        });
    }

    function updateExerciseOptions() {
        const exerciseSelect = document.getElementById('exercise');
        exerciseSelect.innerHTML = '<option value="">Selecione um exercício</option>';
        exercicios.forEach(exercicio => {
            const option = document.createElement('option');
            option.value = exercicio.nome;
            option.textContent = exercicio.nome;
            exerciseSelect.appendChild(option);
        });
    }

    function addFood() {
        const foodSelect = document.getElementById('food');
        const quantityInput = document.getElementById('food-quantity');
        const foodList = document.getElementById('food-list');

        const selectedFood = foodSelect.value;
        const quantity = parseFloat(quantityInput.value);

        if (selectedFood && quantity) {
            const alimento = alimentos.find(a => a.nome === selectedFood);
            const calories = (alimento.caloriasPor100g * quantity) / 100;
            totalCaloriesIngested += calories;

            const listItem = document.createElement('div');
            listItem.className = 'list-item';
            listItem.innerHTML = `
                <span>${selectedFood} - ${quantity}g</span>
                <span>${calories.toFixed(2)} calorias</span>
                <button class="remove-food">Remover</button>
            `;
            foodList.appendChild(listItem);

            updateCaloriesSummary();
        }
    }

    function removeFood(event) {
        if (event.target.classList.contains('remove-food')) {
            const item = event.target.parentElement;
            const calories = parseFloat(item.querySelector('span:nth-child(2)').textContent);
            totalCaloriesIngested -= calories;

            item.remove();
            updateCaloriesSummary();
        }
    }

    function addExercise() {
        const exerciseSelect = document.getElementById('exercise');
        const timeInput = document.getElementById('exercise-time');
        const exerciseList = document.getElementById('exercise-list');

        const selectedExercise = exerciseSelect.value;
        const time = parseFloat(timeInput.value);

        if (selectedExercise && time) {
            const exercicio = exercicios.find(e => e.nome === selectedExercise);
            const calories = exercicio.caloriasPorMinuto * time;
            totalCaloriesBurned += calories;

            const listItem = document.createElement('div');
            listItem.className = 'list-item';
            listItem.innerHTML = `
                <span>${selectedExercise} - ${time} minutos</span>
                <span>${calories.toFixed(2)} calorias</span>
                <button class="remove-exercise">Remover</button>
            `;
            exerciseList.appendChild(listItem);

            updateCaloriesSummary();
        }
    }

    function removeExercise(event) {
        if (event.target.classList.contains('remove-exercise')) {
            const item = event.target.parentElement;
            const calories = parseFloat(item.querySelector('span:nth-child(2)').textContent);
            totalCaloriesBurned -= calories;

            item.remove();
            updateCaloriesSummary();
        }
    }

    function updateCaloriesSummary() {
        const balance = totalCaloriesIngested - totalCaloriesBurned;
        document.getElementById('calories-ingested-today').textContent = totalCaloriesIngested.toFixed(2);
        document.getElementById('calories-burned-today').textContent = totalCaloriesBurned.toFixed(2);
        document.getElementById('calories-balance-today').textContent = balance.toFixed(2);
    }

    document.getElementById('add-food').addEventListener('click', addFood);
    document.getElementById('food-list').addEventListener('click', removeFood);
    document.getElementById('add-exercise').addEventListener('click', addExercise);
    document.getElementById('exercise-list').addEventListener('click', removeExercise);

    document.getElementById('imc-form').addEventListener('submit', function(event) {
        event.preventDefault();
        const weight = parseFloat(document.getElementById('weight').value);
        const height = parseFloat(document.getElementById('height').value);
        if (weight && height) {
            const imc = weight / (height * height);
            let category = '';

            if (imc < 18.5) {
                category = 'Abaixo do peso';
            } else if (imc >= 18.5 && imc < 24.9) {
                category = 'Peso normal';
            } else if (imc >= 25 && imc < 29.9) {
                category = 'Sobrepeso';
            } else {
                category = 'Obesidade';
            }

            document.getElementById('result').textContent = imc.toFixed(2);
            document.getElementById('category').textContent = category;
        }
    });

    updateFoodOptions();
    updateExerciseOptions();
});
```

# CSS

```css
/* Reset e Box-Sizing */
* {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
}

/* Estilo geral do corpo */
body {
  font-family: 'Roboto', sans-serif;
  background-color: #f0f0f0;
  color: #000;
  margin: 0;
  padding: 0;
  display: flex;
  flex-direction: column;
  min-height: 100vh;
  align-items: center;
}

/* Estilo para o cabeçalho */
header {
  width: 100%;
  background-color: #003366;
  color: #fff;
  text-align: center;
  padding: 20px 0;
  flex-shrink: 0;
}

/* Estilo para o rodapé */
footer {
  width: 100%;
  background-color: #003366;
  color: #fff;
  text-align: center;
  padding: 10px 0;
  flex-shrink: 0;
  font-size: 0.8em;
}

/* Caixa de interação */
.interaction-box {
  width: 100%;
  max-width: 600px;
  margin: 20px auto;
  padding: 20px;
  background-color: #fff;
  border-radius: 10px;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
  color: #000;
  box-sizing: border-box;
}

/* Estilo para o título principal */
h1 {
  color: #fff;
  margin: 0;
  font-size: 2.5em;
}

/* Estilo para os subtítulos */
h2, h3 {
  color: #003366;
  margin-bottom: 10px;
  text-align: center;
}

/* Estilo para os botões */
button {
  background-color: #003366;
  color: #fff;
  border: none;
  padding: 10px 20px;
  cursor: pointer;
  border-radius: 4px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  transition: background-color 0.3s ease, transform 0.3s ease;
  margin: 10px auto;
  display: block;
}

button:hover {
  background-color: #002244;
  transform: scale(1.05);
}

/* Estilo para os rótulos */
label {
  display: block;
  margin: 10px 0 5px;
  color: #003366;
  text-align: center;
}

/* Estilo para os campos de entrada e seleção */
input, select {
  width: calc(100% - 20px);
  padding: 10px;
  margin-bottom: 10px;
  border: 1px solid #ccc;
  border-radius: 4px;
  box-sizing: border-box;
}

/* Estilo para as listas de alimentos e exercícios */
#food-list, #exercise-list {
  margin-top: 10px;
  padding: 10px;
  background-color: #e6e6e6;
  border: 1px solid #ccc;
  border-radius: 4px;
}

#food-list li, #exercise-list li {
  padding: 5px 0;
  border-bottom: 1px solid #ddd;
  display: flex;
  justify-content: space-between;
  align-items: center;
}

#food-list li:last-child, #exercise-list li:last-child {
  border-bottom: none;
}

/* Estilo para o botão de remoção */
.remove-button {
  background-color: #ff4d4d;
  color: #fff;
  border: none;
  padding: 5px 10px;
  cursor: pointer;
  border-radius: 4px;
  font-size: 0.9em;
}

.remove-button:hover {
  background-color: #cc0000;
}

/* Resumo de calorias */
#calories-summary {
  margin-top: 20px;
  padding-top: 20px;
  border-top: 1px solid #ccc;
  text-align: center;
}

  text-align: center;
}

input, select {
  width: calc(100% - 20px);
  padding: 10px;
  margin-bottom: 10px;
  border: 1px solid #ccc;
  border-radius: 4px;
  box-sizing: border-box;
}

#food-list, #exercise-list {
  margin-top: 10px;
  padding: 10px;
  background-color: #e6e6e6;
  border: 1px solid #ccc;
  border-radius: 4px;
}

#food-list li, #exercise-list li {
  padding: 5px 0;
  border-bottom: 1px solid #ddd;
  display: flex;
  justify-content: space-between;
  align-items: center;
}

#food-list li:last-child, #exercise-list li:last-child {
  border-bottom: none;
}

.remove-button {
  background-color: #ff4d4d;
  color: #fff;
  border: none;
  padding: 5px 10px;
  cursor: pointer;
  border-radius: 4px;
  font-size: 0.9em;
}

.remove-button:hover {
  background-color: #cc0000;
}

#calories-summary {
  margin-top: 20px;
  padding-top: 20px;
  border-top: 1px solid #ccc;
  text-align: center;
}

