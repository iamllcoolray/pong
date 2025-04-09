ball = {}

function ball.load()
    ball.width = 20
    ball.height = 20
    ball.speed = 200
    ball.velocity = 1
    ball.x = (screen.width - ball.width) / 2
    ball.y = (screen.height - ball.height) / 2
    ball.direction = 1
    ball.angle = 0
end

function ball.update(dt)
    ball.x = ball.x + ball.speed * ball.direction * ball.velocity * dt
    -- ball.y = ball.y + ball.speed * ball.direction * ball.velocity * dt

    if checkCollision(ball, player) then
        ball.direction = -1
        paddle.hits = paddle.hits + 1

        randomValue()
    end

    if checkCollision(ball, enemy) then
        ball.direction = 1
        paddle.hits = paddle.hits + 1

        randomValue()
    end

    if paddle.hits >= 5 then
        if ball.velocity <= 5 then
            ball.velocity = ball.velocity + 0.1
        end
        paddle.hits = 0
    end
end

function ball.draw()
    love.graphics.circle("fill", ball.x, ball.y, ball.width, ball.height)
end

function randomValue()
    if math.random(-1, 1) >= 0 then
        ball.angle = 1
    else
        ball.angle = -1
    end
end
