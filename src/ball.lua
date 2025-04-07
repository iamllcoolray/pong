ball = {}

function ball.load()
    ball.width = 20
    ball.height = 20
    ball.speed = 200
    ball.x = (screen.width - ball.width) / 2
    ball.y = (screen.height - ball.height) / 2
end

function ball.update(dt)

end

function ball.draw()
    love.graphics.circle("fill", ball.x, ball.y, ball.width, ball.height)
end
